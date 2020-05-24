using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receiver
{
    class Program
    {
        const string OUTPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB2\output.txt";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = Settings.RabbitMQHost };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(Settings.QueueName, false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var number = Int64.Parse(Encoding.UTF8.GetString(ea.Body.ToArray()));
                    Console.WriteLine("[x] Received {0}", number);

                    var fibonacciCalculator = new FibonacciCalculator();
                    var result = fibonacciCalculator.Calculate(number, ProcessingType.Sleepy);
                    FileUtility.AppendNumberToFile(OUTPUT_FILE, result);
                };

                channel.BasicConsume(Settings.QueueName, true, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
