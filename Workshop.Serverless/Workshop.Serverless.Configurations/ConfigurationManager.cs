using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;

namespace Workshop.Serverless.Configurations
{
    public class ConfigurationManager
    {
        private string _applicationInsightsKey;
        private string _apiUrl;

        public string ApplicationInsightsKey => _applicationInsightsKey ?? (_applicationInsightsKey = Environment.GetEnvironmentVariable("ApplicationInsightsKey"));

        public string ApiUrl => _apiUrl ?? (_apiUrl = Environment.GetEnvironmentVariable("ApiUrl"));
    }
}
