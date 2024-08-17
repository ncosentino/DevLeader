using Dapper;

using System.Data;
using System.Data.SQLite;

FileInfo testFile = new("test.db");
if (testFile.Exists)
{
    testFile.Delete();
}

using SQLiteConnection connection = new SQLiteConnection(
    $"Data Source='{testFile.FullName}'");
connection.Open();

await CreateTableAsync(connection);

await ClassicInsertAsync(connection, new Entry(1, "Alice", 42));
var entry = await ClassicGetByIdAsync(connection, 1);
Console.WriteLine($"Classic: {entry}");

await DapperInsertAsync(connection, new Entry(2, "Bob", 43));
entry = await DapperGetByIdAsync(connection, 2);
Console.WriteLine($"Dapper: {entry}");

static async Task CreateTableAsync(SQLiteConnection connection)
{
    using SQLiteCommand command = connection.CreateCommand();
    command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS test_table
        (
            id INTEGER PRIMARY KEY,
            name TEXT,
            value INTEGER
        )
        """;
    await command.ExecuteNonQueryAsync();
}

static async Task ClassicInsertAsync(SQLiteConnection connection, Entry entry)
{
    using SQLiteCommand command = connection.CreateCommand();
    command.CommandText =
        "INSERT INTO test_table (id, name, value) VALUES (@id, @name, @value)";
    command.Parameters.AddWithValue("@id", entry.Id);
    command.Parameters.AddWithValue("@name", entry.Name);
    command.Parameters.AddWithValue("@value", entry.Value);
    await command.ExecuteNonQueryAsync();
}

static async Task DapperInsertAsync(IDbConnection connection, Entry entry)
{
    await connection.ExecuteAsync(
        "INSERT INTO test_table (id, name, value) VALUES (@Id, @Name, @Value)",
        entry);
}

static async Task<Entry?> ClassicGetByIdAsync(IDbConnection connection, long id)
{
    using var command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM test_table WHERE id = @id";
    command.Parameters.AddWithValue("@id", id);
    using var reader = await command.ExecuteReaderAsync();
    
    if (await reader.ReadAsync())
    {
        return new Entry(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetInt32(2));
    }

    return null;
}

static async Task<Entry?> DapperGetByIdAsync(IDbConnection connection, long id)
{
    return await connection.QueryFirstOrDefaultAsync<Entry>(
        "SELECT * FROM test_table WHERE id = @id",
        new { id });
}

sealed record Entry(
    long Id, 
    string Name, 
    long Value);