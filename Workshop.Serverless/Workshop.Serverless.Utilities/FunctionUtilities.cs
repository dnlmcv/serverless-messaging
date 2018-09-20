using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Workshop.Serverless.Configurations;

namespace Workshop.Serverless.Utilities
{
    public class FunctionUtilities
    {
        private ConfigurationManager _configurationManager;
        private TelemetryClient _logger;

        public FunctionUtilities(string functionName, Guid executionId)
        {
            Logger.Context.Properties.Add("Operation name",functionName);
            Logger.Context.Properties.Add("Operation id", executionId.ToString());
        }

        public ConfigurationManager ConfigurationManager => _configurationManager ?? (_configurationManager = new ConfigurationManager());

        public TelemetryClient Logger =>
            _logger ?? (_logger = new TelemetryClient(new TelemetryConfiguration(ConfigurationManager.ApplicationInsightsKey)));
    }
}
