using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TestPerfomanceStructAndClass>();

[MemoryDiagnoser]
public class TestPerfomanceStructAndClass
{
    private Struct64 struct64 = new();
    private Struct4 struct4 = new();
    private Class64 class64 = new();
    private Class4 class4  = new();

    [Benchmark]
    public Struct64 TestStruct64()
    {
        for (int i = 0; i < 10000; i++)
        {
            Struct64 res = DoSomebody(struct64);
            if (i == 9999) return res;
        }
        return struct64;
    }

    [Benchmark]
    public Struct64 TestStruct64Ref()
    {
        for (int i = 0; i < 10000; i++)
        {
            Struct64 res = DoSomebodyRef(ref struct64);
            if (i == 9999) return res;
        }
        return struct64;
    }

    [Benchmark]
    public Class64 TestClass64()
    {
        for (int i = 0; i < 10000; i++)
        {
            Class64 res = DoSomebody(class64);
            if (i == 9999) return res;
        }
        return class64;
    }

    [Benchmark]
    public Struct4 Teststruct4()
    {
        for (int i = 0; i < 10000; i++)
        {
            Struct4 res = DoSomebody(struct4);
            if (i == 9999) return res;
        }
        return struct4;
    }

    [Benchmark]
    public Struct4 Teststruct4Ref()
    {
        for (int i = 0; i < 10000; i++)
        {
            Struct4 res = DoSomebodyRef(ref struct4);
            if (i == 9999) return res;
        }
        return struct4;
    }

    [Benchmark]
    public Class4 TestClass4()
    {
        for (int i = 0; i < 10000; i++)
        {
            Class4 res = DoSomebody(class4);
            if (i == 9999) return res;
        }
        return class4;
    }

    private static T DoSomebody<T>(T param)
    {
        return param;
    }

    private static T DoSomebodyRef<T>(ref T param)
    {
        return param;
    }
}

public class Class64
{
    public long l1;
    public long l2;
    public long l3;
    public long l4;
    public long l5;
    public long l6;
    public long l7;
    public long l8;
}
public struct Struct64
{
    public long l1;
    public long l2;
    public long l3;
    public long l4;
    public long l5;
    public long l6;
    public long l7;
    public long l8;
}

public class Class4
{
    public int l2;
}
public struct Struct4
{
    public int l2;
}