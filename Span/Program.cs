

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TestSpan>();

[MemoryDiagnoser]
public class TestSpan
{
    private readonly int[] numbers;

    public TestSpan()
    {
        numbers = new int[100000];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }
    }


    [Benchmark]
    public int SumArraySectionWithoutSpan() => SumArraySection(numbers, 2000, 90000);

    [Benchmark]
    public int SumArraySectionWithSpan() => SumArraySectionWithSpan(numbers, 2000, 90000);

    private static int SumArraySection(int[] array, int start, int end)
    {
        int sum = 0;
        for (int i = start; i <= end; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    private static int SumArraySectionWithSpan(int[] array, int start, int end)
    {
        Span<int> span = array.AsSpan(start, end - start + 1);
        int sum = 0;
        for (int i = 0; i < span.Length; i++)
        {
            sum += span[i];
        }
        return sum;
    }

}

