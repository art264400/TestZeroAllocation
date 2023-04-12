using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<StringInitializationBenchmark>();
public class StringInitializationBenchmark
{
    [Benchmark]
    public string CreateWithStringCreate() => string.Create(10, 0, (span, _) =>
    {
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = (char)('A' + i);
        }
    });

    [Benchmark]
    public unsafe string CreateWithNewStringAndPointers()
    {
        string strNew = new string('\0', 10);
        fixed (char* ptr = strNew)
        {
            for (int i = 0; i < strNew.Length; i++)
            {
                ptr[i] = (char)('A' + i);
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
            charArray[i] = (char)('A' + i);
        }
        return new string(charArray);
    }
}


//string additionalData = "Hello, World!";
//string result = String.Create(5, additionalData, (span, state) =>
//{
//    for (int i = 0; i < span.Length; i++)
//    {
//        span[i] = state[i];
//    }
//});