SomeDependency dependency = new();
SystemUnderTest sut = new(dependency);
var result = sut.FormatWelcomeMessage("Hello");

public sealed class SystemUnderTest
{
    private readonly IDependency _dependency;

    public SystemUnderTest(IDependency dependency)
    {
        _dependency = dependency;
    }

    public string FormatWelcomeMessage(string greeting)
    {
        return $"{greeting} {_dependency.GetSomething()}!";
    }
}

public interface IDependency
{
    string GetSomething();
}

public sealed class SomeDependency : IDependency
{
    public string GetSomething()
    {
        return "Dev Leader";
    }
}