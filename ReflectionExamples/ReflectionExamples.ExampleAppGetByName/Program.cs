using System.Reflection;
var ourAssembly = Assembly.GetExecutingAssembly();
var allTypes = ourAssembly.GetTypes();
var bindingFlags = 
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance |
    BindingFlags.Static;

while (true)
{
    Console.WriteLine("Enter the substring of the type to look for:");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
    {
        break;
    }

    var types = allTypes
        .Where(x => x.Name.Contains(line, StringComparison.OrdinalIgnoreCase))
        .ToArray();
    Console.WriteLine($"Found {types.Length} types!");

    for (int i = 0; i < types.Length; i++)
    {
        Console.WriteLine($"Type {i}: {types[i].Name}");

        Console.WriteLine("Constructors");
        var constructors = types[i].GetConstructors(bindingFlags);

        for (var c = 0; c < constructors.Length; c++)
        {
            var constructor = constructors[c];

            Console.WriteLine($"  Constructor {c}: {constructor.Name}");
            foreach (var parameter in constructor.GetParameters())
            {
                Console.WriteLine($"    Parameter: {parameter.Name} - {parameter.ParameterType}");
            }
        }

        Console.WriteLine("Methods");
        var methods = types[i].GetMethods(bindingFlags);
        for (var m = 0; m < methods.Length; m++)
        {
            var method = methods[m];
            Console.WriteLine($"  Method {m}: {method.Name}");

            foreach (var parameter in method.GetParameters())
            {
                Console.WriteLine($"    Parameter: {parameter.Name} - {parameter.ParameterType}");
            }
        }

        Console.WriteLine("Fields");
        var fields = types[i].GetFields(bindingFlags);
        for (var f = 0; f < fields.Length; f++)
        {
            var field = fields[f];
            Console.WriteLine($"  Field {f}: {field.Name} - {field.FieldType}");
        }

        Console.WriteLine("Properties");
        var properties = types[i].GetProperties(bindingFlags);
        for (var p = 0; p < properties.Length; p++)
        {
            var property = properties[p];
            Console.WriteLine($"  Property {p}: {property.Name} - {property.PropertyType}");
        }
    }
}

public class NicksCoolClass
{
    public void DoSomething()
    {
        Console.WriteLine("I'm doing something!");
    }
}

public class DevLeaderClass
{
    public DevLeaderClass()
        : this(123)
    {
    }

    private DevLeaderClass(
        int count)
    {
        _count = count;
    }

    private string Url => "https://www.youtube.com/@devleader";

    private int _count;
}

public class DevLeaderClass2
{
    public string Url => "https://www.devleader.ca";

    public int Count => 2;

    private static void ThisIsHidden()
    {
        Console.WriteLine("This is hidden!");
    }
}

public class SomeOtherClass
{
    public string Description => "What's this class even for??";

    public DateTime Created => DateTime.Now;
}

public class ThisClassHasNumbers123
{
    public string Name => "This is the name";
    
    public int Number => 123;
}