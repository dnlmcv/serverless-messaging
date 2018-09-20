using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Workshop.Serverless.Utilities;
using Workshop.Shared.Data;

namespace Workshop.Serverless
{
    public static class ASBQueueTrigger
    {
        [FunctionName("ASBQueueTrigger")]
        public static void Run([ServiceBusTrigger("InputQueue", AccessRights.Listen, Connection = "")]BrokeredMessage queueMessage, Microsoft.Azure.WebJobs.ExecutionContext context, CancellationToken cancellationToken)
        {
            var utilities = new FunctionUtilities(context.FunctionName, context.InvocationId);
            var logger = utilities.Logger;

            logger.TrackTrace($"Started function {nameof(ASBQueueTrigger)}",SeverityLevel.Information);


            cancellationToken.Register(() =>
            {
                var exception =
                    new OperationCanceledException("Azure function reached the time limit", cancellationToken);
                logger.TrackException(exception);

                throw exception;
            });


            Event eventObject = new Event();

            try
            {
                eventObject = queueMessage.GetBody<Event>();
            }
            catch (Exception exception)
            {
                logger.TrackException(exception);
            }

            if (string.IsNullOrWhiteSpace(eventObject.User))
            {
                return;
            }

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(utilities.ConfigurationManager.ApiUrl, eventObject);
            }

        }
    }
}
