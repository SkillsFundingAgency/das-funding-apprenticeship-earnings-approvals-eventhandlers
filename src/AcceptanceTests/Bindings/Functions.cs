﻿using System.Threading.Tasks;
using SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Services;
using TechTalk.SpecFlow;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Bindings
{
    [Binding]
    public class Functions
    {
        private readonly TestContext _context;

        public Functions(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario()]
        public async Task InitialiseFunctions()
        {
            _context.ApprovalsFunctions = new TestApprovalsFunctions(_context);
            await _context.ApprovalsFunctions.Start();
       }
    }
}