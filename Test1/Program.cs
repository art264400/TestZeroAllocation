using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using LinkDotNet.StringBuilder;
using System.Text;

BenchmarkRunner.Run<StringBanchmark>();


//unsafe
//{
//    char* chars = stackalloc char[10]; // выделение памяти на стеке для хранения массива символов
//    for (int i = 0; i < 10; i++)
//    {
//        *(chars + i) = 'a'; // наполнение массива символов символами 'a'
//    }
//    string str = new string(chars, 0, 10); // создание строки на основе массива символов
//}

[MemoryDiagnoser]
public class StringBanchmark
{
    private string str1, str2, str3, str4, str5;

    public StringBanchmark()
    {
        str1 = new string('a', 50);
        str2 = new string('a', 150);
        str3 = new string('a', 300);
        str4 = new string('a', 1000);
        str5 = new string('a', 1500);
    }

    [Benchmark]
    public string StringPlus()
    {
        string str = string.Empty; 
        for(int i = 0; i < 100; i++)
        {
            str += str1 + str2 + str3 + str4 + str5;
        }
        return str;
        //return
        //    str1 + str2 + str3 + str4 + str5; //string.Concat(str1, str2, str3, str4, str5);
    }

    [Benchmark]
    public string StringBuilder()
    {
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < 100; i++)
        {
            stringBuilder.Append(str1);
            stringBuilder.Append(str2);
            stringBuilder.Append(str3);
            stringBuilder.Append(str4);
            stringBuilder.Append(str5);
        }
      
        return stringBuilder.ToString();
    }

    [Benchmark]
    public string ValueStringBuilder()
    {
        var stringBuilder = new ValueStringBuilder();
        for(int i = 0; i < 100; i++)
        {
            stringBuilder.Append(str1);
            stringBuilder.Append(str2);
            stringBuilder.Append(str3);
            stringBuilder.Append(str4);
            stringBuilder.Append(str5);
        }
        return stringBuilder.ToString();
    }
}