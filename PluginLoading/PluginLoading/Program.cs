using System.Reflection;

using Autofac;

using ExampleProgram;

internal sealed class Program
{
    private static void Main(string[] args)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule<TestModule>();
        containerBuilder.RegisterType<ThingThatDoesWork>();

        // load plugins dynamically
        var assemblies = Directory
            .GetFiles(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "*plugin*.dll")
            .Select(x => Assembly.LoadFile(x))
            .ToArray();
        foreach (var assembly in assemblies)
        {
            containerBuilder.RegisterDiscoverableTypes(assembly);
        }

        var container = containerBuilder.Build();

        var printer = container.Resolve<ThingThatDoesWork>();
        printer.Print();
        Console.ReadLine();
    }

    public sealed class ThingThatDoesWork
    {
        private readonly IRepository _repository;

        public ThingThatDoesWork(IRepository repository)
        {
            _repository = repository;
        }

        public void Print()
        {
            foreach (var obj in _repository.GetAllObjects())
            {
                Console.WriteLine(obj);
            }
        }
    }
}
