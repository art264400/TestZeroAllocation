using LibraryTest;
using System;


namespace _9_3._TestTargetNetCore3._1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var fileOperation = new StringOperations();
            var str = fileOperation.ConcatenateStrings(new string[] { "a", "r", "t" });
            Console.WriteLine(str);
        }
    }
}
