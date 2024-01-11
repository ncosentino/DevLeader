RecordExample();
ClassicConstructorClassExample();
PrimaryConstructorClassExample();


void RecordExample()
{
    MyRecord record = new(1, "Hello");

    // does not work
    record.Name = "World";
    //record.Id = 2;

    Console.WriteLine(
        $"{record.Id} - {record.Name}");
}

void ClassicConstructorClassExample()
{
    ClassicConstructorClass theClass = new(1, "Hello");

    // does not work
    //theClass.Name = "World";
    //theClass.Id = 2;

    Console.WriteLine(
        $"{theClass.Id} - {theClass.Name}");
}

void PrimaryConstructorClassExample()
{
    PrimaryConstructorClass theClass = new(1, "Hello");

    // does not work
    //theClass.Name = "World";
    //theClass.Id = 2;
    
    // does not work because the parameters are fields!
    //Console.WriteLine(
    //    $"{theClass.Id} - {theClass.Name}");
    theClass.Print();
}

public sealed record MyRecord(
    int Id,
    string Name);

public sealed class ClassicConstructorClass
{
    public ClassicConstructorClass(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; }
}

public sealed class PrimaryConstructorClass(
    int Id,
    string Name)
{
    public void Print()
    {
        Console.WriteLine(
            $"{Id} - {Name}");
                
        Id = 1337;
    }
}



/*
public sealed class MyConnectionFactory()
{
}

public sealed class MyRepository(
    MyConnectionFactory _myConnectionFactory)
{ 
}

public sealed class MyBusinessLogic(
    MyRepository _myRepository)
{
    public void DoTheThing()
    {
        //_myRepository.DoStuff()
    }
}
*/