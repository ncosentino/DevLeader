Console.WriteLine("Constructors");
var constructors = typeof(MyClass).GetConstructors();
foreach (var (constructor, index) in constructors
    .Select((constructor, index) => (constructor, index)))
{
    Console.WriteLine($"  Constructor {index}: {constructor.Name}");
    foreach (var parameter in constructor.GetParameters())
    {
        Console.WriteLine($"    Parameter: {parameter.Name} - {parameter.ParameterType}");
    }
}

Console.WriteLine("Properties");
var properties = typeof(MyClass).GetProperties();
foreach (var (property, index) in properties
       .Select((property, index) => (property, index)))
{
    Console.WriteLine($"  Property {index}: {property.Name} - {property.PropertyType}");
}

Console.WriteLine("Methods");
var methods = typeof(MyClass).GetMethods();
foreach (var (method, index) in methods
       .Select((method, index) => (method, index)))
{
    Console.WriteLine($"  Method {index}: {method.Name}");
    foreach (var parameter in method.GetParameters())
    {
        Console.WriteLine($"    Parameter: {parameter.Name} - {parameter.ParameterType}");
    }
}

public class MyClass
{
    public MyClass()
        : this(123, "ABC")
    {        
    }

    public MyClass(
        int numericProperty,
        string stringProperty)
    {
        NumericProperty = numericProperty;
        StringProperty = stringProperty;
    }

    public int NumericProperty { get; set; }

    public string StringProperty { get; set; }

    public void MyMethod()
    {
        // TODO: do some cool stuff!
    }

    public void MyMethodWithParams(
        int someNumber, 
        string someString)
    {
        // TODO: do some cool stuff!
    }
}