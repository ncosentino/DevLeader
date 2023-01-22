public sealed class MyObjectRepositoryFacade : IRepository
{
    private readonly IReadOnlyCollection<IRepository> _repositories;

    public MyObjectRepositoryFacade(IEnumerable<IRepository> repositories)
    {
        _repositories = repositories.ToArray();
    }

    public IEnumerable<MyObject> GetAllObjects()
        => _repositories.SelectMany(x => x.GetAllObjects());
}