using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using System.Data;

var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

var summaries = BenchmarkSwitcher
    .FromAssembly(typeof(BenchmarkFixture).Assembly)
    .Run(args, config)
    .ToArray();

public class SampleDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options) :
        base(options)
    {
    }
}

public record User(
    int Id, 
    string Username);

public sealed class BenchmarkFixture
{
    private SqliteConnection? _connection;
    private SampleDbContext? _context;

    public void Setup(int numberOfEntriesInDataset)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<SampleDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        _context = new SampleDbContext(options);
        _context.Database.EnsureCreated();

        for (int i = 1; i <= numberOfEntriesInDataset; i++)
        {
            User user = new(i, $"User {i}");
            _context.Users.Add(user);
        }

        _context.SaveChanges();

        var count = _context.Users.Count();
        if (count != numberOfEntriesInDataset)
        {
            throw new Exception("Database not seeded correctly.");
        }
    }

    public List<User> GetAllUsersFullyMaterializedEfCore()
    {
        var users = _context!.Users.ToList();
        return users;
    }

    public IEnumerable<User> GetAllUsersIteratorEfCore()
    {
        var users = _context!.Users;
        return users;
    }

    public IEnumerable<User> GetUsersPagedEfCore(
        int offset,
        int pageSize)
    {
        var users = _context!.Users.Skip(offset).Take(pageSize);
        return users;
    }

    public IReadOnlyList<User> GetUsersPagedRawSql(
        int offset,
        int pageSize)
    {
        using var cmd = _connection!.CreateCommand();
        cmd.CommandText = "SELECT * FROM Users LIMIT @pageSize OFFSET @offset";
        cmd.Parameters.Add(new SqliteParameter("@pageSize", DbType.Int32) { Value = pageSize });
        cmd.Parameters.Add(new SqliteParameter("@offset", DbType.Int32) { Value = offset });

        using var reader = cmd.ExecuteReader();
        var users = new List<User>(pageSize);
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var username = reader.GetString(1);
            users.Add(new User(id, username));
        }

        return users;
    }

    public List<User> GetUsersPagedRawSqlAsList(
        int offset,
        int pageSize)
    {
        using var cmd = _connection!.CreateCommand();
        cmd.CommandText = "SELECT * FROM Users LIMIT @pageSize OFFSET @offset";
        cmd.Parameters.Add(new SqliteParameter("@pageSize", DbType.Int32) { Value = pageSize });
        cmd.Parameters.Add(new SqliteParameter("@offset", DbType.Int32) { Value = offset });
        
        using var reader = cmd.ExecuteReader();
        var users = new List<User>(pageSize);
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var username = reader.GetString(1);
            users.Add(new User(id, username));
        }

        return users;
    }

    public IEnumerable<User> GetAllUsersIteratorRawSql()
    {
        using SqliteCommand cmd = _connection!.CreateCommand();
        cmd.CommandText = "SELECT * FROM Users";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var username = reader.GetString(1);
            yield return new User(id, username);
        }
    }

    public List<User> GetAllUsersFullyMaterializedRawSql()
    {
        using SqliteCommand cmd = _connection!.CreateCommand();
        cmd.CommandText = "SELECT * FROM Users";

        using var reader = cmd.ExecuteReader();
        var users = new List<User>();
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var username = reader.GetString(1);
            users.Add(new User(id, username));            
        }

        return users;
    }
}

[ShortRunJob]
[MemoryDiagnoser]
public class AnyBenchmarks
{
    private readonly BenchmarkFixture _fixture = new();

    [Params(
        1_000,
        100_000,
        10_000_000)]
    public int NumberOfEntriesInDataset;

    [GlobalSetup]
    public void Setup() => _fixture.Setup(NumberOfEntriesInDataset);

    [Benchmark]
    public bool Any_FullyMaterializedEfCore()
    {
        return _fixture.GetAllUsersFullyMaterializedEfCore().Count != 0;
    }

    [Benchmark]
    public bool Any_FullyMaterializedRawSql()
    {
        return _fixture.GetAllUsersFullyMaterializedRawSql().Count != 0;
    }

    [Benchmark]
    public bool Any_IteratorEfCore()
    {
        return _fixture.GetAllUsersIteratorEfCore().Any();
    }

    [Benchmark]
    public bool Any_IteratorRawSql()
    {
        return _fixture.GetAllUsersIteratorRawSql().Any();
    }

    [Benchmark]
    public bool Any_PagedEfCore()
    {
        return _fixture.GetUsersPagedEfCore(0, 1).Any();
    }

    [Benchmark]
    public bool Any_PagedRawSql()
    {
        return _fixture.GetUsersPagedRawSql(0, 1).Count != 0;
    }

    [Benchmark]
    public bool Any_PagedRawSqlAsList()
    {
        return _fixture.GetUsersPagedRawSqlAsList(0, 1).Count != 0;
    }
}

[ShortRunJob]
[MemoryDiagnoser]
public class CountBenchmarks
{
    private readonly BenchmarkFixture _fixture = new();

    [Params(
        1_000,
        100_000,
        10_000_000)]
    public int NumberOfEntriesInDataset;

    [GlobalSetup]
    public void Setup() => _fixture.Setup(NumberOfEntriesInDataset);

    [Benchmark]
    public int Count_FullyMaterializedEfCore()
    {
        return _fixture.GetAllUsersFullyMaterializedEfCore().Count;
    }

    [Benchmark]
    public int Count_FullyMaterializedRawSql()
    {
        return _fixture.GetAllUsersFullyMaterializedRawSql().Count;
    }

    [Benchmark]
    public int Count_IteratorEfCore()
    {
        return _fixture.GetAllUsersIteratorEfCore().Count();
    }

    [Benchmark]
    public int Count_IteratorRawSql()
    {
        return _fixture.GetAllUsersIteratorRawSql().Count();
    }

    [Benchmark]
    public int Count_PagedEfCore()
    {
        return _fixture.GetUsersPagedEfCore(0, 1).Count();
    }

    [Benchmark]
    public int Count_PagedRawSql()
    {
        return _fixture.GetUsersPagedRawSql(0, 1).Count;
    }

    [Benchmark]
    public int Count_PagedRawSqlAsList()
    {
        return _fixture.GetUsersPagedRawSqlAsList(0, 1).Count;
    }
}

[ShortRunJob]
[MemoryDiagnoser]
public class FullyMaterializedBenchmarks
{
    private readonly BenchmarkFixture _fixture = new();

    [Params(
        1_000,
        100_000,
        10_000_000)]
    public int NumberOfEntriesInDataset;

    [GlobalSetup]
    public void Setup() => _fixture.Setup(NumberOfEntriesInDataset);

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_FullyMaterializedEfCore()
    {
        return _fixture.GetAllUsersFullyMaterializedEfCore();
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_FullyMaterializedRawSql()
    {
        return _fixture.GetAllUsersFullyMaterializedRawSql();
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_IteratorEfCore()
    {
        return _fixture.GetAllUsersIteratorEfCore().ToArray();
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_IteratorRawSql()
    {
        return _fixture.GetAllUsersIteratorRawSql().ToArray();
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_PagedEfCore()
    {
        return _fixture.GetUsersPagedEfCore(0, NumberOfEntriesInDataset).ToArray();
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_PagedRawSql()
    {
        return _fixture.GetUsersPagedRawSql(0, NumberOfEntriesInDataset);
    }

    [Benchmark]
    public IReadOnlyList<User> GetFullMaterializedSet_PagedRawSqlAsList()
    {
        return _fixture.GetUsersPagedRawSqlAsList(0, NumberOfEntriesInDataset);
    }
}



































[ShortRunJob]
[MemoryDiagnoser]
public class PagingBenchmarks
{
    private readonly BenchmarkFixture _fixture = new();

    [Params(
        1_000,
        100_000,
        10_000_000)]
    public int NumberOfEntriesInDataset;

    [GlobalSetup]
    public void Setup() => _fixture.Setup(NumberOfEntriesInDataset);

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_FullyMaterializedEfCore()
    {
        var results = new List<User>(100);
        foreach (var user in _fixture
            .GetAllUsersFullyMaterializedEfCore()
            .Take(100))
        {
            results.Add(user);
        }

        return results;
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_FullyMaterializedRawSql()
    {
        var results = new List<User>(100);
        foreach (var user in _fixture
            .GetAllUsersFullyMaterializedRawSql()
            .Take(100))
        {
            results.Add(user);
        }

        return results;
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_IteratorEfCore()
    {
        var results = new List<User>(100);
        foreach (var user in _fixture
            .GetAllUsersIteratorEfCore()
            .Take(100))
        {
            results.Add(user);
        }

        return results;
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_IteratorRawSql()
    {
        var results = new List<User>(100);
        foreach (var user in _fixture
            .GetAllUsersIteratorRawSql()
            .Take(100))
        {
            results.Add(user);
        }

        return results;
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_PagedEfCore()
    {
        var results = new List<User>(100);
        foreach (var user in _fixture.GetUsersPagedEfCore(0, 100))
        {
            results.Add(user);
        }

        return results;
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_PagedRawSql()
    {
        return _fixture.GetUsersPagedRawSql(0, 100);
    }

    [Benchmark]
    public IReadOnlyList<User> GetPageOf100_PagedRawSqlAsList()
    {
        return _fixture.GetUsersPagedRawSqlAsList(0, 100);
    }
}