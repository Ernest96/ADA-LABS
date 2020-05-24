using Akka.Actor;
using Common;
using System;

namespace LAB1_AKKA
{
    public class FibonacciActor: UntypedActor
    {
        protected override void OnReceive(object message)
        {
            ProcessFibonacci((FibonacciMessage)message);
        }

        protected override void PreStart()
        {
            //Console.WriteLine("Actor started");
        }

        protected override void PostStop()
        {
            //Console.WriteLine("Actor stoped ");
        }

        private void ProcessFibonacci(FibonacciMessage message)
        {
            var fibonacciCalculator = new FibonacciCalculator();

            for (int i = 0; i < message.NumbersToProcess; i++)
            {
                var numberIndex = message.StartIndex + i;
                var result = fibonacciCalculator.Calculate(message.FibonacciNumbers[numberIndex], message.ProcessingType);
                SynchronizationContext.AddNumberAndCheckIfIsDone(result);
            }
        }
    }
}
