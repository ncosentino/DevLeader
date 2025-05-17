namespace CacheExamples.Repositories.Common;

public sealed record Entity(
    long Id,
    string Value) :
    BaseEntity(Id);