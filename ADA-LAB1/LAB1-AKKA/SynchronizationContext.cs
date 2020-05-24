using Common;
using System;
using System.Diagnostics;
using System.Threading;

namespace LAB1_AKKA
{
    public static class SynchronizationContext
    {
        const string OUTPUT_FILE = @"D:\Programming\master\ADA\ADA-LABS\ADA-LAB1\output-actor.txt";

        private static int _numbersToProcess;
        private static int _numbersProcessed;
        private static Stopwatch _stopWatch;

        public static void InitAndStartWatch(int numbersToProcess)
        {
            _numbersToProcess = numbersToProcess;
            _numbersProcessed = 0;
            _stopWatch = Stopwatch.StartNew();
        }

        public static void AddNumberAndCheckIfIsDone(long number)
        {
            ConcurrentStore.AddNumberToStore(number);
            Interlocked.Increment(ref _numbersProcessed);

            if (_numbersProcessed >= _numbersToProcess)
            {
                _stopWatch.Stop();
                Console.WriteLine($"{_stopWatch.ElapsedMilliseconds} milliseconds");

                var result = ConcurrentStore.GetSortedNumbersFromStore();
                FileUtility.WriteNumbersToFile(OUTPUT_FILE, result);

                ConcurrentStore.ClearStore();
                _numbersProcessed = 0;
            }
        }
    }
}
