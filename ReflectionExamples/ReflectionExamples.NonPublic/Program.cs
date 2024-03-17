using System.Reflection;

var type = typeof(MyClass);
Console.WriteLine(type.Name);

//var constructors = type.GetConstructors();
var constructors = type.GetConstructors(
    BindingFlags.NonPublic |
    BindingFlags.Instance);


Console.WriteLine($"Constructors: {constructors.Length}");
foreach (var constructor in constructors)
{
    foreach (var parameter in constructor.GetParameters())
    {
        Console.WriteLine($"  Parameter: {parameter.Name} - {parameter.ParameterType}");
    }
}

Console.WriteLine("Making an instance...");
var instance = (MyClass)constructors[0].Invoke(null);
Console.WriteLine($"Instance created: {instance}");

var methods = type.GetMethods(
    BindingFlags.NonPublic |
    BindingFlags.Instance);
Console.WriteLine($"Methods: {methods.Length}");
foreach (var method in methods)
{
    Console.WriteLine(method.Name);
}

var secretMethod = methods.Single(m => m.Name == "MyMethod");
Console.WriteLine($"Top Secret Method: {secretMethod}");
secretMethod.Invoke(instance, null);

Console.WriteLine("Getting the private fields!");
var fields = type.GetFields(
    BindingFlags.NonPublic |
    BindingFlags.Instance);
Console.WriteLine($"Fields: {fields.Length}");

Console.WriteLine("Setting the private fields!");
var numericField = fields.Single(f => f.Name == "_numericValue");
numericField.SetValue(instance, 123);

var stringValueField = fields.Single(f => f.Name == "_stringValue");
stringValueField.SetValue(instance, "ABC");

Console.WriteLine("Calling the private method again!");
secretMethod.Invoke(instance, null);


public class MyClass
{
    // haha! nobody can make an instance of this!
    private MyClass()
    {
    }

    // nobody can see these!
    private string _stringValue;
    private int _numericValue;

    // nobody can call this!!
    private void MyMethod()
    {
        Console.WriteLine("This is super top secret.");
        Console.WriteLine($"{nameof(_stringValue)} - {_stringValue}");
        Console.WriteLine($"{nameof(_numericValue)} - {_numericValue}");
    }
}