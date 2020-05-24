using System;
using System.Threading;

namespace Common
{
    public enum ProcessingType
    {
        Sleepy,
        Busy,
        Unknown = 99
    }

    public class FibonacciCalculator
    {
        public long Calculate(long number, ProcessingType processingType)
        {
            if (processingType == ProcessingType.Busy)
                return BusyFibonacci(number);
            else if (processingType == ProcessingType.Sleepy)
                return SleepyFibonacci(number);

            throw new Exception("Unsuported processing type");
        }

        public static ProcessingType GetProcessingType()
        {
            Console.Write("Choose Processing Type (s - sleepy b - busy): ");
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

        private long SleepyFibonacci(long number)
        {
            KeepCpuAsleep();
            return ComputeFibonacci(number);
        }

        private long BusyFibonacci(long number)
        {
            KeepCpuBusy();
            return ComputeFibonacci(number);
        }

        private void KeepCpuAsleep()
        {
            var sleep_milisec = 250;
            Thread.Sleep(sleep_milisec);
        }

        private int KeepCpuBusy()
        {
            int iterations_count = 13000;
            int k = 0;
            for (int i = 0; i < iterations_count; i++)
            {
                for (int j = 0; j < iterations_count; j++)
                {
                    k++;
                }
            }
            return k;
        }

        private long ComputeFibonacci(long number)
        {
            if (number <= 0)
            {
                Console.WriteLine($"Error! Passed number is negative {number}. Expected only positive number as input.");
                Environment.Exit(1);
            }

            if (number == 1)
                return 0;
            if (number == 2)
                return 1;

            long number1 = 0, number2 = 1;
            long fib = 0;

            for (int i = 3; i <= number; i++)
            {
                fib = number1 + number2;
                number1 = number2;
                number2 = fib;
            }

            return fib;
        }
    }
}
