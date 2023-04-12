using System;

namespace _9_8_1._TestTargetLibrary
{
    public class MyLibrary
    {
        public static void DoSomething()
        {
#if NET6_0_OR_GREATER
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