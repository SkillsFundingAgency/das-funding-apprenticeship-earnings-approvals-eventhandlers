﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Hooks;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Services
{
    public class TestHelper
    {
        private readonly TestContext _testContext;

        public TestHelper(TestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task<WaitForResult> WaitFor<T>(
                   Func<Task> func,
                   bool assertOnTimeout = true,
                   bool assertOnError = false,
                   int timeoutInMs = 15000)
        {
            var waitForResult = new WaitForResult();

            var hook = (Hook<T>)_testContext.Hooks.Single(h => h is Hook<T>);

            hook.OnReceived = (message) => { waitForResult.SetHasStarted(); };
            hook.OnProcessed = (message) => { waitForResult.SetHasCompleted(); };
            hook.OnErrored = (ex, message) => { waitForResult.SetHasErrored(ex); };

            try
            {
                await func();
            }
            catch (Exception ex)
            {
                waitForResult.SetHasErrored(ex);
            }
            await WaitForHandlerCompletion(waitForResult, timeoutInMs);

            if (assertOnTimeout)
            {
                waitForResult.HasTimedOut.Should().Be(false, "handler should not have timed out");
            }

            if (assertOnError)
            {
                waitForResult.HasErrored.Should().Be(false, $"handler should not have errored with error '{waitForResult.LastException?.Message}'");
            }

            return waitForResult;
        }

        private async Task WaitForHandlerCompletion(WaitForResult waitForResult, int timeoutInMs)
        {
            using (Timer timer = new Timer(new TimerCallback(TimedOutCallback), waitForResult, timeoutInMs, Timeout.Infinite))
            {
                while (!waitForResult.HasCompleted && !waitForResult.HasTimedOut)
                {
                    await Task.Delay(100);
                }
            }
        }

        private void TimedOutCallback(object? state)
        {
            ((WaitForResult)state!).SetHasTimedOut();
        }
    }
}
