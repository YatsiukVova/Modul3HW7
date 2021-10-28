using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Modul3HW7
{
    public class Program
    {
        public static async Task Show(int i)
        {
            Console.WriteLine(i);
        }

        public static void Main(string[] args)
        {
            // №1
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var result1 = new List<Task>();
            result1.Add(Task.Run(() => Show(1)));
            result1.Add(Task.Run(() => Show(2)));
            result1.Add(Task.Run(() => Show(3)));
            await Task.WhenAll(result1);
            foreach (var task in result1)
            {
                Console.WriteLine(((Task<int>)task).Result);
            }

        // №2
        var timer = 10000;
            cancellationTokenSource.CancelAfter(timer);

            try
            {
                var task = Task.Run(() => FibonacсiAsync(50, token));

                var result = task.GetAwaiter().GetResult();

                Console.WriteLine($"Fibonacci is {result}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static async Task<int> FibonacсiAsync(int index, CancellationToken token)
        {
            return await Task.Run(() => Fibonacсi(index, token));
        }

        public static int Fibonacсi(int index, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (index == 0 || index == 1)
            {
                return index;
            }

            return Fibonacсi(index - 1, token) + Fibonacсi(index - 2, token);
        }
    }
}
