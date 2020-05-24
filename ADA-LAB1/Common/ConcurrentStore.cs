using System.Collections.Concurrent;
using System.Linq;

namespace Common
{
    public static class ConcurrentStore
    {
        private static readonly ConcurrentBag<long> _numbersBag = new ConcurrentBag<long>();

        public static void AddNumberToStore(long number)
        {
            _numbersBag.Add(number);
        }

        public static long[] GetSortedNumbersFromStore()
        {
            var sortedNumbers = _numbersBag.OrderBy(x => x).ToArray();

            return sortedNumbers;
        }

        public static void ClearStore()
        {
            _numbersBag.Clear();
        }
    }
}
