using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Workshop.Shared.Data;

namespace Workshop.Messaging
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ServiceBusConnectionString = "<your_connection_string>";
            const string QueueName = "<your_queue_name>";

            var queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            while (true)
            {
                Console.ReadLine();

                queueClient.SendAsync(new Message(ObjectToByteArray(new Event()
                {
                    ProcessInstanceId = Guid.NewGuid(),
                    ProcessName = "Test Process",
                    Step = "START",
                    User = Guid.NewGuid().ToString()
                })));

            }
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
