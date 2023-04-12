using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var result = BenchmarkRunner.Run<MemoryVsArrayCopy>();

/// 
/// Здесь нельзя было бы использовать Span, потому что у нас идет async await
/// В принципе этот код по производительности будет схож
/// главное понять, что здесь 

public class MemoryVsArrayCopy
{

    private readonly int DataSize = 100000;
    private readonly int[] _dataArray;
    private Memory<int> _dataMemory;

    public MemoryVsArrayCopy()
    {
        _dataArray = new int[DataSize];
        _dataMemory = new Memory<int>(_dataArray);
    }

    [Benchmark]
    public async Task ProcessArrayWithCopyAsync()
    {
        int[] dataCopy = new int[DataSize / 2];
        Array.Copy(_dataArray, dataCopy, DataSize / 2);
        await ProcessDataAsync(dataCopy);
    }

    [Benchmark]
    public async Task ProcessMemoryWithoutCopyAsync()
    {
        Memory<int> dataSlice = _dataMemory.Slice(0, DataSize / 2);
        await ProcessDataAsync(dataSlice);
    }

    private async Task ProcessDataAsync(int[] data)
    {
        await Task.Delay(20);
        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= 2;
        }
    }

    private async Task ProcessDataAsync(Memory<int> data)
    {
        await Task.Delay(20);
        for (int i = 0; i < data.Length; i++)
        {
            data.Span[i] *= 2;
        }
    }
}
