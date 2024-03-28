using BenchmarkDotNet.Attributes;

using MongoDB.Bson;
using MongoDB.Driver;

using Testcontainers.MongoDb;

[MemoryDiagnoser]
//[ShortRunJob]
[MediumRunJob]
public class InsertBenchmarks
{
    private MongoDbContainer? _container;
    private MongoClient? _mongoClient;
    private IMongoCollection<BsonDocument>? _collection;
    private IMongoCollection<RecordStructDto>? _collectionRecordStruct;
    private IMongoCollection<RecordClassDto>? _collectionRecordClass;
    private IMongoCollection<StructDto>? _collectionStruct;
    private IMongoCollection<ClassDto>? _collectionClass;

    [GlobalSetup]
    public async Task SetupAsync()
    {
        _container = new MongoDbBuilder()
            .WithImage("mongo:latest")
            .Build();
        await _container.StartAsync();
        
        _mongoClient = new MongoClient(_container.GetConnectionString());
        var database = _mongoClient.GetDatabase("test");
        _collection = database.GetCollection<BsonDocument>("test");
        _collectionRecordStruct = database.GetCollection<RecordStructDto>("test");
        _collectionRecordClass = database.GetCollection<RecordClassDto>("test");
        _collectionStruct = database.GetCollection<StructDto>("test");
        _collectionClass = database.GetCollection<ClassDto>("test");
    }

    [GlobalCleanup]
    public async Task CleanupAsync()
    {
        await _container!.StopAsync();
    }

    [Benchmark]
    public async Task InsertOneAsync_BsonDocument()
    {
        await _collection!.InsertOneAsync(new BsonDocument()
        {
            ["Name"] = "Nick Cosentino",
        });
    }

    [Benchmark]
    public async ValueTask InsertOneAsyncValueTask_BsonDocument()
    {
        await _collection!.InsertOneAsync(new BsonDocument()
        {
            ["Name"] = "Nick Cosentino",
        });
    }

    [Benchmark]
    public void InsertOne_BsonDocument()
    {
        _collection!.InsertOne(new BsonDocument()
        {
            ["Name"] = "Nick Cosentino",
        });
    }

    [Benchmark]
    public async Task InsertOneAsync_RecordStruct()
    {
        await _collectionRecordStruct!.InsertOneAsync(new RecordStructDto("Nick Cosentino"));
    }

    [Benchmark]
    public async ValueTask InsertOneAsyncValueTask_RecordStruct()
    {
        await _collectionRecordStruct!.InsertOneAsync(new RecordStructDto("Nick Cosentino"));
    }

    [Benchmark]
    public void InsertOne_RecordStruct()
    {
        _collectionRecordStruct!.InsertOne(new RecordStructDto("Nick Cosentino"));
    }

    [Benchmark]
    public async Task InsertOneAsync_RecordClass()
    {
        await _collectionRecordClass!.InsertOneAsync(new RecordClassDto("Nick Cosentino"));
    }

    [Benchmark]
    public async ValueTask InsertOneAsyncValueTask_RecordClass()
    {
        await _collectionRecordClass!.InsertOneAsync(new RecordClassDto("Nick Cosentino"));
    }

    [Benchmark]
    public void InsertOne_RecordClass()
    {
        _collectionRecordClass!.InsertOne(new RecordClassDto("Nick Cosentino"));
    }

    [Benchmark]
    public async Task InsertOneAsync_Struct()
    {
        await _collectionStruct!.InsertOneAsync(new StructDto() { Name = "Nick Cosentino" });
    }

    [Benchmark]
    public async ValueTask InsertOneAsyncValueTask_Struct()
    {
        await _collectionStruct!.InsertOneAsync(new StructDto() { Name = "Nick Cosentino" });
    }

    [Benchmark]
    public void InsertOne_Struct()
    {
        _collectionStruct!.InsertOne(new StructDto() { Name = "Nick Cosentino" });
    }

    [Benchmark]
    public async Task InsertOneAsync_Class()
    {
        await _collectionClass!.InsertOneAsync(new ClassDto() { Name = "Nick Cosentino" });
    }

    [Benchmark]
    public async ValueTask InsertOneAsyncValueTask_Class()
    {
        await _collectionClass!.InsertOneAsync(new ClassDto() { Name = "Nick Cosentino" });
    }

    [Benchmark]
    public void InsertOne_Class()
    {
        _collectionClass!.InsertOne(new ClassDto() { Name = "Nick Cosentino" });
    }

    private record struct RecordStructDto(string Name);

    private record class RecordClassDto(string Name);

    private struct StructDto
    {
        public string Name { get; set; }
    }

    private class ClassDto
    {
        public string Name { get; set; }
    }
}