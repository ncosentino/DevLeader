using Autofac;

[DiscoverableForRegistration(DiscoveryRegistrationMode.ImplementedInterfaces, true)]
public sealed class InMemoryRepositoryForPlugin1 : IRepository
{
    private readonly IReadOnlyCollection<MyObject> _objects;

    public InMemoryRepositoryForPlugin1()
    {
        _objects = new MyObject[]
        {
            new MyObject("Plugin1 - A"),
            new MyObject("Plugin1 - B"),
            new MyObject("Plugin1 - C"),
        };
    }

    public IEnumerable<MyObject> GetAllObjects()
        => _objects;
}
