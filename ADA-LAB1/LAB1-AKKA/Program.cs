using Akka.Actor;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LAB1_AKKA
{
    public class Program
    {
        const string INPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB1\input.txt";

        static void Main(string[] args)
        {
            var fibonacciNumbers = FileUtility.ReadNumbersFromFile(INPUT_FILE);

            var processingType = FibonacciCalculator.GetProcessingType();

            //Console.Write("Enter the number of Actors: ");
            //var numberOfActors = Int32.Parse(Console.ReadLine());

            //Console.WriteLine($"\nRuning with {numberOfActors} Actors.");
            //ProcessFibonacci(fibonacciNumbers, numberOfActors, processingType);

            Task.Run(() =>
            {
                for (int i = 1; i <= 32; i *= 2)
                {
                    Console.WriteLine($"\nRuning with {i} Actors.");
                    ProcessFibonacci(fibonacciNumbers, i, processingType);
                    Thread.Sleep(25 * 1000);
                }
            });


            Console.ReadLine();
        }

        private static void ProcessFibonacci(long[] fibonacciNumbers, int numberOfActors, ProcessingType processingType)
        {
            var actorSystem = ActorSystem.Create($"Fibonacci-Actors-{numberOfActors}");
            var actors = new List<IActorRef>(numberOfActors);
            var messages = new List<FibonacciMessage>(numberOfActors);

            for (int i = 0; i < numberOfActors; i++)
            {
                int taskIndex = i;
                var actor = actorSystem.ActorOf(Props.Create<FibonacciActor>());

                int numbersPerTask = fibonacciNumbers.Length / numberOfActors; // ideal case, even tasks and even numbers count
                int numbersToProcess = 0;

                if (taskIndex == numberOfActors - 1) // if is last actor
                    numbersToProcess = fibonacciNumbers.Length - (numbersPerTask * (numberOfActors - 1));
                else
                    numbersToProcess = numbersPerTask;

                var message = new FibonacciMessage()
                {
                    FibonacciNumbers = fibonacciNumbers,
                    NumbersToProcess = numbersToProcess,
                    StartIndex = taskIndex * numbersPerTask,
                    ProcessingType = processingType
                };

                actors.Add(actor);
                messages.Add(message);
            }

            SynchronizationContext.InitAndStartWatch(fibonacciNumbers.Length);

            for (int i = 0; i < numberOfActors; i++)
                actors[i].Tell(messages[i]);
        }
    }
}
