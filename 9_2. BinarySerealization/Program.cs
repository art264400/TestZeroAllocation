using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MessagePack;
using System.Text.Json;
using MemoryPack;

BenchmarkRunner.Run<SerializationBenchmarks>();

[MessagePackObject]
[MemoryPackable]
public partial class MyClass
{
    [Key(0)]
    public int Id { get; set; }
    [Key(1)]
    public string Name { get; set; }
}

public class SerializationBenchmarks
{
    private readonly MyClass _testObject = new MyClass { Id = 1, Name = "Arthur" };
    private readonly byte[] _messagePackData;
    private readonly byte[] _memoryPackData;
    private readonly string _jsonData;

    public SerializationBenchmarks()
    {
        _messagePackData = MessagePackSerializer.Serialize(_testObject);
        _jsonData = JsonSerializer.Serialize(_testObject);
        _memoryPackData = MemoryPackSerializer.Serialize(_testObject);
    }

    [Benchmark]
    public byte[] MessagePackSerialize() => MessagePackSerializer.Serialize(_testObject);

    [Benchmark]
    public MyClass MessagePackDeserialize() => MessagePackSerializer.Deserialize<MyClass>(_messagePackData);

    [Benchmark]
    public string JsonSerialize() => JsonSerializer.Serialize(_testObject);

    [Benchmark]
    public MyClass JsonDeserialize() => JsonSerializer.Deserialize<MyClass>(_jsonData);


    [Benchmark]
    public byte[] MemoryPackSerialize() => MemoryPackSerializer.Serialize(_testObject);

    [Benchmark]
    public MyClass? MemoryPackDeserialize() => MemoryPackSerializer.Deserialize<MyClass>(_memoryPackData);
}