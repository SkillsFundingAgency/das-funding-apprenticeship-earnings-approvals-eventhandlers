﻿using System;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.AcceptanceTests.Hooks
{  
    public class Hook<T> : IHook<T>
    {
        public Action<T>? OnReceived { get; set; }
        public Action<T>? OnProcessed { get; set; }
        public Action<Exception, T>? OnErrored { get; set; }
    }
}
