using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ADA_LAB1
{
    class Program
    {
        const string INPUT_FILE = @"D:\Programming\master\ADA-LAB1\ADA-LAB1\input.txt";
        const string OUTPUT_FILE = @"D:\Programming\master\ADA-LAB1\ADA-LAB1\output.txt";

        static async Task Main(string[] args)
        {
            var fibonacciNumbers = FileUtility.ReadNumbersFromFile(INPUT_FILE);

            var processingType = GetProcessingType();

            for (int i = 1; i <= 32; i *= 2)
            {
                Console.WriteLine($"\nRuning with {i} Threads.");
                var result = await ProcessFibonacci(fibonacciNumbers, i, processingType);
                FileUtility.WriteNumbersToFile(OUTPUT_FILE, result);
            }

            Console.ReadLine();
        }

        private static async Task<long[]> ProcessFibonacci(long[] fibonacciNumbers, int tasksNumber, ProcessingType processingType)
        {
            var tasks = new List<Task>(tasksNumber);

            for (int i = 0; i < tasksNumber; i++)
            {
                int taskIndex = i; //concurrency workaround for captured vars
                var task = new Task(() =>
                {
                    var fibonacciCalculator = new FibonacciCalculator();
                    int numbersPerTask = fibonacciNumbers.Length / tasksNumber; // ideal case, even tasks and even numbers count
                    int numbersToProcess = 0;

                    if (taskIndex == tasksNumber - 1) // if is last task
                        numbersToProcess = fibonacciNumbers.Length - (numbersPerTask * (tasksNumber - 1));
                    else
                        numbersToProcess = numbersPerTask;

                    for(int i = 0; i < numbersToProcess; i++)
                    {
                        var numberIndex = taskIndex * numbersPerTask + i;
                        var result = fibonacciCalculator.Calculate(fibonacciNumbers[numberIndex], processingType);
                        ConcurrentStore.AddNumberToStore(result);
                    }

                });
                tasks.Add(task);
            }

            var watch = Stopwatch.StartNew();

            tasks.ForEach(x => x.Start());
            await Task.WhenAll(tasks);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"{tasksNumber} Threads: {elapsedMs} milliseconds");

            var numbers = ConcurrentStore.GetSortedNumbersFromStore();
            ConcurrentStore.ClearStore();

            return numbers;
        }


        private static ProcessingType GetProcessingType()
        {
            Console.WriteLine("Choose Processing Type (s - sleepy b - busy)");
            string choice = Console.ReadLine();
            ProcessingType processingType = ProcessingType.Unknown; 

            if (choice == "b")
            {
                processingType = ProcessingType.Busy;
            }
            else if (choice == "s")
            {
                processingType = ProcessingType.Sleepy;
            }
            else
            {
                Console.WriteLine("Unsupported type");
                Environment.Exit(1);
            }


            return processingType;
        }
    }
}
