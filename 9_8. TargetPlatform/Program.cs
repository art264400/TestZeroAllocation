using System;

namespace _9_8._TargetPlatform
{
    public class TestPlatform
    {
        public static void DoSomething()
        {
#if NETCOREAPP6_0_OR_GREATER
            Console.WriteLine("Using .NET 6 or greater features");
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] slicedNumbers = numbers[1..^1];
            foreach (int number in slicedNumbers)
            {
                Console.WriteLine(number);
            }
#else
            Console.WriteLine("Using .NET Core 3.1 or other older frameworks");
#endif
        }
    }
}
