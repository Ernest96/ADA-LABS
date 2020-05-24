using Common;

namespace LAB1_AKKA
{
    public class FibonacciMessage
    {
        public long[] FibonacciNumbers { get; set; }

        public int NumbersToProcess { get; set; }

        public int StartIndex { get; set; }

        public ProcessingType ProcessingType {get; set;}
    }
}
