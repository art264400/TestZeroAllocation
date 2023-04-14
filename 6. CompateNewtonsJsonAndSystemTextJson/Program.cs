using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;

BenchmarkRunner.Run<JsonBenchmarks>();
public class JsonBenchmarks
{
    private readonly SampleData _sampleData = new SampleData
    {
        Id = 1,
        Name = "Sample",
        CreatedAt = DateTime.UtcNow
    };

    private readonly string _json = "{\"Id\":1,\"Name\":\"Sample\",\"CreatedAt\":\"2023-04-13T00:00:00Z\"}";

    [Benchmark]
    public string NewtonsoftJsonSerialize() => JsonConvert.SerializeObject(_sampleData);

    [Benchmark]
    public string SystemTextJsonSerialize() => System.Text.Json.JsonSerializer.Serialize(_sampleData);

    [Benchmark]
    public SampleData NewtonsoftJsonDeserialize() => JsonConvert.DeserializeObject<SampleData>(_json);

    [Benchmark]
    public SampleData SystemTextJsonDeserialize() => System.Text.Json.JsonSerializer.Deserialize<SampleData>(_json);
}

public class SampleData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}