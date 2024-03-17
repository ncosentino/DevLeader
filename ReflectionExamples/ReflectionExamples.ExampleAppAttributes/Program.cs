using System.Reflection;

var typesWithOurAttribute = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.GetCustomAttributes<MyClassAttribute>().Any())
    .ToArray();

Console.WriteLine(
    $"There are {typesWithOurAttribute.Length} " +
    $"types with the MyClassAttribute");
foreach (var type in typesWithOurAttribute)
{
    var attribute = type.GetCustomAttribute<MyClassAttribute>()!;
    Console.WriteLine($"Type: {type.Name}:");
    Console.WriteLine($"  Name: {attribute.Name}");
    Console.WriteLine($"  Description: {attribute.Description}");
}

Console.WriteLine();

var methodsWithOurAttribute = typesWithOurAttribute
    .SelectMany(t => t.GetMethods())
    .Where(m => m.GetCustomAttributes<MyMethodAttribute>().Any())
    .ToArray();
Console.WriteLine(
    $"There are {methodsWithOurAttribute.Length} " +
    $"methods with the MyMethodAttribute");
foreach (var method in methodsWithOurAttribute)
{
    var attribute = method.GetCustomAttribute<MyMethodAttribute>()!;
    Console.WriteLine($"{method.DeclaringType!.Name}.{method.Name}:");
    Console.WriteLine($"  Name: {attribute.Name}");
    Console.WriteLine($"  Description: {attribute.Description}");
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class MyClassAttribute : Attribute
{
    public MyClassAttribute(
        string name, 
        string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }
}

[MyClass(nameof(ClassA), "This is a class")]
public sealed class ClassA
{

}

public sealed class ClassB
{
    // No attributes on anything!
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class MyMethodAttribute : Attribute
{
    public MyMethodAttribute(
        string name, 
        string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }

    public string Description { get; }
}

[MyClass(
    nameof(ClassC),
    "This is a class. This one has a special method!")]
public sealed class ClassC
{
    [MyMethod(nameof(MethodA), "This is a method")]
    public void MethodA()
    {
    }

    public void MethodB()
    {
    }
}