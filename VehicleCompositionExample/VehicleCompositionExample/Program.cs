// ---------------
// You can find the associated video for this code here:
// https://youtu.be/iEfuyxwKQCE
// Find more full length articles at:
// https://www.devleader.ca
// ---------------

Vehicle myMotorcycle = new Vehicle(
    new GenericEngine(
        2,
        "1000cc"),
    Array.Empty<IDoor>(),
    new IWheel[]
    {
        new GenericWheel(16, 6),
        new GenericWheel(15, 9),
    });

Vehicle myTruck = new Vehicle(
    new NicksUniqueTruckEngine(),
        new IDoor[]
    {
        new AlarmDoor(),
        new AlarmDoor(),
        new GenericDoor(),
        new GenericDoor(),
    },
    new IWheel[]
    {
        new GenericWheel(24, 12),
        new GenericWheel(24, 12),
        new GenericWheel(24, 12),
        new GenericWheel(24, 12),
    });

// driver-side front door
myTruck.Doors.First().Lock();
myTruck.Doors.First().Open();

// passenger-side rear door
myTruck.Doors.Last().Lock();
myTruck.Doors.Last().Open();

public interface IDoor
{
    bool IsOpen { get; }

    bool IsLocked { get; }

    void Open();

    void Close();

    void Lock();

    void Unlock();
}

public interface IEngine
{
    int NumberOfCylinders { get; }

    string Displacement { get; }
}

public interface IWheel
{
    int Width { get; }

    int Diameter { get; }
}

public sealed record Vehicle(
    IEngine Engine,
    IDoor[] Doors,
    IWheel[] Wheels);

public sealed record GenericEngine(
    int NumberOfCylinders,
    string Displacement)
    : IEngine;

public sealed record GenericWheel(
    int Diameter,
    int Width)
    : IWheel;

public sealed record GenericDoor()
    : IDoor
{
    public bool IsOpen
    { get; private set; }

    public bool IsLocked
    { get; private set; }

    public void Close()
        => IsOpen = false;

    public void Lock()
    => IsLocked = true;

    public void Open()
    {
        if (IsOpen)
        {
            return;
        }

        if (IsLocked)
        {
            Console.WriteLine("The door is locked!");
            return;
        }

        IsOpen = true;
    }

    public void Unlock()
        => IsLocked = false;
}

public sealed record AlarmDoor()
    : IDoor
{
    public bool IsOpen
    { get; private set; }

    public bool IsLocked
    { get; private set; }

    public void Close()
        => IsOpen = false;

    public void Lock()
    => IsLocked = true;

    public void Open()
    {
        if (IsOpen)
        {
            return;
        }

        if (IsLocked)
        {
            Console.WriteLine("*LOUD ALARM BLARING!!!!*");
            return;
        }

        IsOpen = true;
    }

    public void Unlock()
        => IsLocked = false;
}

public sealed record NicksUniqueTruckEngine
    : IEngine
{
    public int NumberOfCylinders
        => 8;

    public string Displacement
        => "10L of awesomeness!";
}