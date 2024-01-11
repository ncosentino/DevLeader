using System.Text;

while (true)
{
    Console.Write("Enter a string to encode: ");
    var input = Console.ReadLine();

    var bytes = Encoding.UTF8.GetBytes(input);

    var base64String = Convert.ToBase64String(bytes);

    Console.WriteLine($"Base64 encoded string: {base64String}");
    Console.WriteLine();

    Console.Write("Enter a string to decode: ");
    input = Console.ReadLine();
    
    var decoded = Convert.FromBase64String(input);
    var utf8Decoded = Encoding.UTF8.GetString(decoded);

    Console.WriteLine($"Base64 decoded string: {utf8Decoded}");
}