MyRepository repository = new();
MyAppLayerService service = new(repository);
UIControl uiControl = new(service);
await uiControl.RefreshListContentsAsync();


public sealed record Data(
    int Value);

public sealed class PretendConnectionFactory
{
    public PretendConnection OpenNewConnection()
    {
        Console.WriteLine("Opening new connection");
        return new();
    }
}

public sealed class PretendConnection : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Connection is disposing");
    }
}

public sealed class MyRepository
{
    private readonly PretendConnectionFactory _connectionFactory;

    public MyRepository()
    {
        _connectionFactory = new();
    }

    public IEnumerable<Data> GetData()
    {
        Console.WriteLine("Starting GetData");
        try
        {
            using PretendConnection connection = _connectionFactory
                .OpenNewConnection();
            
            // BROKEN
            //return ReadData(connection);
            
            foreach (var data in ReadData(connection))
            {
                yield return data;
            }
        }
        finally
        {
            Console.WriteLine("Ending GetData");
        }
    }

    private IEnumerable<Data> ReadData(PretendConnection connection)
    {
        var preserveColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Starting iterator inside ReadData");

        // pretend we're doing something with the connection
        // to get the data that we're returning here... this
        // could be using something like Dapper, EF Core, or
        // even just a DataReader.

        yield return new(1);
        yield return new(2);
        yield return new(3);

        Console.WriteLine("Ending iterator inside ReadData");
        Console.ForegroundColor = preserveColor;
    }
}







public sealed class UIControl
{
    private readonly MyAppLayerService _service;

    public UIControl(MyAppLayerService service)
    {
        _service = service;
    }

    public void RefreshListContents()
    {
        Console.WriteLine("Starting RefreshListContents on the UI thread!");

        foreach (var data in _service.GetData())
        {
            Console.WriteLine($"Data: {data.Value}");
        }

        Console.WriteLine("Ending RefreshListContents");
    }

    public async Task RefreshListContentsAsync()
    {
        Console.WriteLine("Starting RefreshListContentsAsync on the UI thread!");

        var records = await Task.Run(() =>
        {
            Console.WriteLine("Starting Task.Run");
            var data = _service.GetData();
            Console.WriteLine("Ending Task.Run");
            return data;
        });

        Console.WriteLine("Hopefully we did that all asynchronously, right?!");
        foreach (var data in records)
        {
            Console.WriteLine($"Data: {data.Value}");
        }

        Console.WriteLine("Ending RefreshListContentsAsync");
    }
}

public sealed class MyAppLayerService
{
    private readonly MyRepository _repository;

    public MyAppLayerService(MyRepository repository)
    {
        _repository = repository;
    }

    // note that this is kind of nasty "bleeding" entities
    // from the app layer into the UI layer...
    public IEnumerable<Data> GetData()
    {
        return _repository.GetData();
    }
}