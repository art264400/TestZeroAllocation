using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ArraySectionBenchmark>();


//В этом примере мы сравниваем два подхода для обработки сегмента массива int. 
//В методе ProcessArraySectionWithCopy мы копируем сегмент исходного массива в новый массив, а затем обрабатываем его.
//В методе ProcessArraySectionWithMemory мы создаем объект Memory<int> для представления сегмента массива и обрабатываем его 
//без копирования данных.

[MemoryDiagnoser]
public class ArraySectionBenchmark
{
    private readonly int[] data;

    public ArraySectionBenchmark()
    {
        data = new int[100_000];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = i;
        }
    }

    [Benchmark]
    public int[] ProcessArraySectionWithCopy()
    {
        //здесь мы копируем массив
        int[] result = new int[50_000];
        Array.Copy(data, 25_000, result, 0, 50_000);
        return ProcessArraySection(result);
    }

    [Benchmark]
    public int[] ProcessArraySectionWithMemory()
    {
        //здесь уже работает с памятью, без копий
        Memory<int> memory = data.AsMemory(25_000, 50_000);
        return ProcessArraySection(memory);
    }

    [Benchmark]
    public int[] ProcessArraySectionWithSpan()
    {
        //здесь уже работает с памятью, без копий
        Span<int> memory = data.AsSpan(25_000, 50_000);
        return ProcessArraySection(memory);
    }

    private int[] ProcessArraySection(int[] array)
    {
        int[] result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i] * 2;
        }
        return result;
    }

    private int[] ProcessArraySection(Memory<int> memory)
    {
        int[] result = new int[memory.Length];
        for (int i = 0; i < memory.Length; i++)
        {
            result[i] = memory.Span[i] * 2;
        }
        return result;
    }

    private int[] ProcessArraySection(Span<int> span)
    {
        int[] result = new int[span.Length];
        for (int i = 0; i < span.Length; i++)
        {
            result[i] = span[i] * 2;
        }
        return result;
    }
}

//В целом, выбор между Span<T> и Memory<T> зависит от сценария использования и того, как вы собираетесь работать с данными.
//Если вам нужно работать только внутри одного метода и/или с неуправляемой памятью/стеком, выбирайте Span<T>. 
//Если вам нужно передавать представление памяти между методами, 
//хранить его в полях или использовать асинхронный код, выбирайте Memory<T>.

