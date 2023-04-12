using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringInitializationBenchmark>();

//var sda = new StringInitializationBenchmark();
//var str = new string('\0', 10);
//Console.WriteLine($"Length: {str.Length}"); // Output: Length: 10
//Console.WriteLine(str);

// При простом наполнеие с повторяющимеся символами более эффективно стандартное выделение 

public class StringInitializationBenchmark
{
    [Benchmark]
    public string CreateWithStringCreate() => String.Create(10, '_', (span, fillChar) =>
    {
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = fillChar;
        }
    });

    [Benchmark]
    public unsafe string CreateWithNewStringAndPointers()
    {
        // заполняем строку нулевыми символами
        string strNew = new string('\0', 10);
        fixed (char* ptr = strNew)
        {
            for (int i = 0; i < strNew.Length; i++)
            {
                ptr[i] = '_';
            }
        }
        return strNew;
    }

    [Benchmark]
    public string CreateWithNewStringAndCharArray()
    {
        char[] charArray = new char[10];
        for (int i = 0; i < charArray.Length; i++)
        {
            charArray[i] = '_';
        }
        return new string(charArray);
    }

    [Benchmark]
    public string CreateWithNewString()
    {
        return new string('_', 10);
    }
}

