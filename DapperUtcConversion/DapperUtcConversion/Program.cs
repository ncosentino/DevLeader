using Dapper;

using MySql.Data.MySqlClient;

using System.Data;

using Xunit;

SqlMapper.AddTypeHandler(new DateTimeHandler());

#region password
var password = "N3Xu5L4B5";
#endregion
using var connection = new MySqlConnection(
    $"Server=localhost;" +
    $"Database=dapper_utc_conversion;" +
    $"User Id=ncosentino;" +
    $"Password={password}");
connection.Open();

var now = DateTime.UtcNow;
await connection.ExecuteAsync("DELETE FROM our_table");
await connection.ExecuteAsync(
    "INSERT INTO our_table (datetime) VALUES (@ourValue)", 
    new { ourValue = now });

var results = await connection.QueryAsync<OurRecord>("SELECT * FROM our_table");

Console.WriteLine("Results:");
foreach (var result in results)
{
    Console.WriteLine(result);

    DateTime dateTime = result.datetime;
    Assert.Equal(now, dateTime, TimeSpan.FromMilliseconds(100));

    DateTimeOffset dateTimeOffset = result.datetime;
    Assert.Equal(now, dateTimeOffset, TimeSpan.FromMilliseconds(100));

    Console.WriteLine($"Difference: {result.datetime - now}");
    Console.WriteLine($"Kind: {result.datetime.Kind}");
}

internal sealed record OurRecord(
    DateTime datetime);

internal sealed class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(
        IDbDataParameter parameter,
        DateTime value)
    {
        parameter.Value = value;
    }

    public override DateTime Parse(object value)
    {
        return DateTime.SpecifyKind(
            (DateTime)value,
            DateTimeKind.Utc);
    }
}