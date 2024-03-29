﻿using System;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Services
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope() { }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
