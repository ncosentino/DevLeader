MyClass first = new MyClass()
{
    X = 1,
    Y = 2,
};
MyClass second = new MyClass()
{
    X = 1,
    Y = 2,
};
Console.WriteLine($"MyClass: == {first == second}");
Console.WriteLine($"MyClass: object.Equals() {object.Equals(first, second)}");

MyRecord firstRecord = new MyRecord(1, 2);
MyRecord secondRecord = new MyRecord(1, 2);
Console.WriteLine($"MyRecord: == {firstRecord == secondRecord}");
Console.WriteLine($"MyRecord: object.Equals() {object.Equals(firstRecord, secondRecord)}");

class MyClass
{
    public int X { get; set; }
    public int Y { get; set; }
}

record MyRecord(
    int X,
    int Y);