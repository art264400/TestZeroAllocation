using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestCpuLock
{
    internal class Program
    {
        private static object locker = new object();    
        static void Main(string[] args)
        {
            Parallel.For(0, 10000, body =>
            {

                lock (locker)
                {
                    Thread.Sleep(30000);
                    Console.WriteLine(body);
                }
                
            });
        }
    }
}
