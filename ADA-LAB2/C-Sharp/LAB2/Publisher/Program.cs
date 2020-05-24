using Common;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        const string INPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB2\input.txt";
        const string OUTPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB2\output.txt";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = Settings.RabbitMQHost };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Settings.QueueName, false, false, false, null);

                var fibonacciNumbers = FileUtility.ReadNumbersFromFile(INPUT_FILE);
                FileUtility.RecreateFile(OUTPUT_FILE);

                foreach (var fibonacciNumber in fibonacciNumbers)
                {
                    var body = Encoding.UTF8.GetBytes(fibonacciNumber.ToString());

                    channel.BasicPublish("", Settings.QueueName, null, body);
                    Console.WriteLine("[x] Sent {0}", fibonacciNumber);

                    //TODO: refactor ???
                    Thread.Sleep(250);  // Waiting to write in the file to avoid incosisten data 
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
