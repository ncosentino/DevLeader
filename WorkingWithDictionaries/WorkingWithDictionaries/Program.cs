var people = new Person[]
{
    new("John", 25),
    new("Joe", 26),
    new("Jimmy", 25)
};

Dictionary<Person, int> peopleByAge = people
    .ToDictionary(p => p, p => p.Age);

foreach (var item in peopleByAge)
{
    Console.WriteLine(
        $"{item.Key}: {item.Value}");
}


/* What happens when hashcodes collide?
 * The primarily downside of hashcode 
 * collisions, is that it reduces the 
 * dictionary into a list in terms of 
 * performance. Whenever two different 
 * object instances yield the same hash 
 * code, they are stored in the same 
 * internal bucket of the dictionary.
 */
sealed record Person(
    string Name,
    int Age)
{
    public override int GetHashCode()
    {
        // NOTE: this is demonstrating a
        // bad implementation of GetHashCode
        var hashCode = Age;
        Console.WriteLine($"{this}: HashCode={hashCode}");
        return hashCode;
    }
}