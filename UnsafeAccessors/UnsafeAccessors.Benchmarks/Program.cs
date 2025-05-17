using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Reflection;
using System.Runtime.CompilerServices;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

public class OurType
{
    public int InstanceField;

    public int InstanceMethod()
    {
        return InstanceField;
    }

    public int InstanceProperty { get; set; }
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_InstantiationBenchmarks
{
    [Benchmark(Baseline = true)]
    public OurType Constructor_Classic()
    {
        return new OurType();
    }

    [Benchmark]
    public OurType Constructor_ActivatorCreateInstance()
    {
        return Activator.CreateInstance<OurType>();
    }

    [Benchmark]
    public OurType Constructor_Unsafe()
    {
        return UnsafeConstructor();
    }

    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    extern static OurType UnsafeConstructor();
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_GetFieldBenchmarks
{
    private FieldInfo? _instanceFieldInfo;
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
        _instanceFieldInfo = typeof(OurType).GetField("InstanceField");
    }

    [Benchmark(Baseline = true)]
    public int InstanceField_Classic()
    {
        return _instance!.InstanceField;
    }

    [Benchmark]
    public int InstanceField_Reflection()
    {
        return (int)_instanceFieldInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        return instanceField;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_SetFieldBenchmarks
{
    private OurType? _instance;
    private FieldInfo? _instanceFieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
        _instanceFieldInfo = typeof(OurType).GetField("InstanceField");
    }

    [Benchmark(Baseline = true)]
    public void InstanceField_Classic()
    {
        _instance!.InstanceField = 123456;
    }

    [Benchmark]
    public void InstanceField_Reflection()
    {
        _instanceFieldInfo!.SetValue(_instance, 123456);
    }

    [Benchmark]
    public void InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        instanceField = 123456;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_GetPropertyBenchmarks
{
    private OurType? _instance;
    private PropertyInfo? _instancePropertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
        _instancePropertyInfo = typeof(OurType).GetProperty("InstanceProperty")!;
    }

    [Benchmark(Baseline = true)]
    public int InstanceProperty_Classic()
    {
        return _instance!.InstanceProperty;
    }

    [Benchmark]
    public int InstanceProperty_Reflection()
    {
        return (int)_instancePropertyInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int InstanceProperty_Unsafe()
    {
        return GetInstanceProperty(_instance!);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_InstanceProperty")]
    extern static int GetInstanceProperty(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_SetPropertyBenchmarks
{
    private OurType? _instance;
    private PropertyInfo? _instancePropertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
        _instancePropertyInfo = typeof(OurType).GetProperty("InstanceProperty")!;
    }

    [Benchmark(Baseline = true)]
    public void InstanceProperty_Classic()
    {
        _instance!.InstanceProperty = 123456;
    }

    [Benchmark]
    public void InstanceProperty_Reflection()
    {
        _instancePropertyInfo!.SetValue(_instance, 123456);
    }

    [Benchmark]
    public void InstanceProperty_Unsafe()
    {
        SetInstanceProperty(_instance!, 123456);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_InstanceProperty")]
    extern static void SetInstanceProperty(
        OurType instance,
        int value);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_MethodBenchmarks
{
    private OurType? _instance;
    private MethodInfo? _instanceMethodInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
        _instanceMethodInfo = typeof(OurType).GetMethod("InstanceMethod")!;
    }

    [Benchmark(Baseline = true)]
    public int InstanceMethod_Classic()
    {
        return _instance!.InstanceMethod();
    }

    [Benchmark]
    public int InstanceMethod_Unsafe()
    {
        return InvokeMethod(_instance!);
    }

    [Benchmark]
    public int InstanceMethod_Reflection()
    {
        return (int)_instanceMethodInfo!.Invoke(_instance, null)!;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "InstanceMethod")]
    extern static int InvokeMethod(
        OurType instance);
}