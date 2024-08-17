using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Dapper;

using System.Data;
using System.Data.SQLite;
using System.Reflection;

BenchmarkRunner.Run(Assembly.GetExecutingAssembly());

[MemoryDiagnoser]
[ShortRunJob]
public class InsertBenchmarks
{
    private IDbConnection? _connection;

    [Params(1, 100, 1000, 10000)]
    public int RecordsCount { get; set; }

    [IterationSetup]
    public void Setup()
    {
        if (File.Exists("test.db"))
        {
            File.Delete("test.db");
        }

        _connection = new SQLiteConnection("Data Source=test.db");
        _connection.Open();
        CreateTable(_connection);
    }


    [IterationCleanup]
    public void Cleanup()
    {
        _connection?.Dispose();
    }

    [Benchmark]
    public void ClassicInsert()
    {
        for (int i = 0; i < RecordsCount; i++)
        {
            ClassicInsert(_connection!, new Entry(i, "Alice", 42));
        }
    }

    [Benchmark]
    public async Task DapperInsert_Async()
    {
        for (int i = 0; i < RecordsCount; i++)
        {
            await DapperInsertAsync(_connection!, new Entry(i, "Alice", 42));
        }
    }

    [Benchmark]
    public void DapperInsert_Sync()
    {
        for (int i = 0; i < RecordsCount; i++)
        {
            DapperInsert(_connection!, new Entry(i, "Alice", 42));
        }
    }

    private static void CreateTable(IDbConnection connection)
    {
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS test_table1
            (
                id INTEGER PRIMARY KEY,
                name TEXT,
                value INTEGER
            );
            CREATE TABLE IF NOT EXISTS test_table2
            (
                id INTEGER PRIMARY KEY,
                name TEXT,
                value INTEGER
            );
            CREATE TABLE IF NOT EXISTS test_table3
            (
                id INTEGER PRIMARY KEY,
                name TEXT,
                value INTEGER
            );
            ";
        command.ExecuteNonQuery();
    }

    private static void ClassicInsert(IDbConnection connection, Entry entry)
    {
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            "INSERT INTO test_table1 (id, name, value) VALUES (@id, @name, @value)";
        command.Parameters.Add(new SQLiteParameter("@id", entry.Id));
        command.Parameters.Add(new SQLiteParameter("@name", entry.Name));
        command.Parameters.Add(new SQLiteParameter("@value", entry.Value));
        command.ExecuteNonQuery();
    }

    private static async Task DapperInsertAsync(IDbConnection connection, Entry entry)
    {
        await connection.ExecuteAsync(
            "INSERT INTO test_table2 (id, name, value) VALUES (@Id, @Name, @Value)",
            entry);
    }

    private static void DapperInsert(IDbConnection connection, Entry entry)
    {
        connection.Execute(
            "INSERT INTO test_table3 (id, name, value) VALUES (@Id, @Name, @Value)",
            entry);
    }
}

public record Entry(long Id, string Name, int Value);