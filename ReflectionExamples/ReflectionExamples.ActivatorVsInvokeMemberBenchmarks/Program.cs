//
// This code was written for the following Dev Leader content:
// https://www.devleader.ca/2024/03/14/activator-createinstance-vs-type-invokemember-a-clear-winner/
// https://www.devleader.ca/2024/03/17/constructorinfo-how-to-make-reflection-in-dotnet-faster-for-instantiation/
// https://youtu.be/Djq7eMI_L-4 
//
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Reflection;

BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
//BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).RunAllJoined();

public class ParameterlessClass
{
}

public class ClassicStringParameterClass
{
    private readonly string _value;

    public ClassicStringParameterClass(string value)
    {
        _value = value;
    }
}

public class PrimaryConstructorStringParameterClass(
    string _value)
{
}

[ShortRunJob]
public class ParameterlessClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ParameterlessClass);
        _constructorInfo = _type.GetConstructor(Type.EmptyTypes);
    }

    [Benchmark]
    public void Constructor()
    {
        var instance = new ParameterlessClass();
    }

    [Benchmark(Baseline = true)]
    public void Activator_Create_Instance()
    {
        var instance = Activator.CreateInstance(_type!);
    }

    [Benchmark]
    public void Type_Invoke_Member()
    {
        var instance = _type!.InvokeMember(
            null,
            BindingFlags.CreateInstance,
            null,
            null,
            null);
    }

    [Benchmark]
    public void Constructor_Info_Invoke()
    {
        var instance = _constructorInfo!.Invoke(null);
    }

    [Benchmark]
    public void Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor(Type.EmptyTypes);
        var instance = constructorInfo!.Invoke(null);
    }
}

[ShortRunJob]
public class ClassicStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ClassicStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark]
    public void Constructor()
    {
        var instance = new ClassicStringParameterClass("Hello World!");
    }

    [Benchmark(Baseline = true)]
    public void Activator_Create_Instance()
    {
        var instance = Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public void Type_Invoke_Member()
    {
        var instance = _type!
            .InvokeMember(
                null,
                BindingFlags.CreateInstance,
                null,
                null,
                new[]
                {
                    "Hello World!",
                });
    }

    [Benchmark]
    public void Constructor_Info_Invoke()
    {
        var instance = _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }

    [Benchmark]
    public void Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor([typeof(string)]);
        var instance = constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}

[ShortRunJob]
public class PrimaryConstructorStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(PrimaryConstructorStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark]
    public void Constructor()
    {
        var instance = new PrimaryConstructorStringParameterClass("Hello World!");
    }

    [Benchmark(Baseline = true)]
    public void Activator_Create_Instance()
    {
        var instance = Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public void Type_Invoke_Member()
    {
        var instance = _type!
            .InvokeMember(
                null,
                BindingFlags.CreateInstance,
                null,
                null,
                new[]
                {
                    "Hello World!",
                });
    }

    [Benchmark]
    public void Constructor_Info_Invoke()
    {
        var instance = _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }

    [Benchmark]
    public void Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor([typeof(string)]);
        var instance = constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}