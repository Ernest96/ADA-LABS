using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ADA_LAB1
{
    class Program
    {
        const string INPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB1\input.txt";
        const string OUTPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB1\output-tpl.txt";

        static async Task Main(string[] args)
        {
            var fibonacciNumbers = FileUtility.ReadNumbersFromFile(INPUT_FILE);
            var processingType = FibonacciCalculator.GetProcessingType();

            for (int i = 1; i <= 32; i *= 2)
            {
                Console.WriteLine($"\nRuning with {i} Threads.");
                var result = await ProcessFibonacci(fibonacciNumbers, i, processingType);
                FileUtility.WriteNumbersToFile(OUTPUT_FILE, result);
            }

            Console.ReadLine();
        }

        private static async Task<long[]> ProcessFibonacci(long[] fibonacciNumbers, int numberOfTasks, ProcessingType processingType)
        {
            var tasks = new List<Task>(numberOfTasks);

            for (int i = 0; i < numberOfTasks; i++)
            {
                int taskIndex = i; //concurrency workaround for captured vars
                var task = new Task(() =>
                {
                    var fibonacciCalculator = new FibonacciCalculator();
                    int numbersPerTask = fibonacciNumbers.Length / numberOfTasks; // ideal case, even tasks and even numbers count
                    int numbersToProcess = 0;

                    if (taskIndex == numberOfTasks - 1) // if is last task
                        numbersToProcess = fibonacciNumbers.Length - (numbersPerTask * (numberOfTasks - 1));
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
            Console.WriteLine($"{numberOfTasks} Threads: {watch.ElapsedMilliseconds} milliseconds");

            var numbers = ConcurrentStore.GetSortedNumbersFromStore();
            ConcurrentStore.ClearStore();

            return numbers;
        }
    }
}
