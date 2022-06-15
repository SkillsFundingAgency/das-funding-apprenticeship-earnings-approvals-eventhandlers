using SFA.DAS.Http.Configuration;

namespace SFA.DAS.Funding.ApprenticeshipEarnings.Approvals.EventHandlers.Functions.Configuration
{
    public class ApprenticeshipEarningsApiOptions : IApimClientConfiguration
    {
        public const string ApprenticeshipEarningsApi = "ApprenticeshipEarningsApi";
        public string ApiBaseUrl { get; set; }
        public string SubscriptionKey { get; set; }
        public string ApiVersion { get; set; }
    }
}
