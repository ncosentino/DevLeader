using Autofac;

[DiscoverableForRegistration(DiscoveryRegistrationMode.ImplementedInterfaces, true)]
public sealed class InMemoryRepositoryForPlugin2 : IRepository
{
    private readonly IReadOnlyCollection<MyObject> _objects;

    public InMemoryRepositoryForPlugin2()
    {
        _objects = new MyObject[]
    {
            new MyObject("Plugin2 - 1"),
            new MyObject("Plugin2 - 2"),
            new MyObject("Plugin2 - 3"),
    };
    }

    public IEnumerable<MyObject> GetAllObjects()
        => _objects;
}
