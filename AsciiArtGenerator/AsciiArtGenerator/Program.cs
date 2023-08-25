using AsciiArtGenerator;

string imagePath = "Your file path here";
using var inputStream = new FileStream(
    imagePath, 
    FileMode.Open, 
    FileAccess.Read,
    FileShare.Read);
var generator = new Generator();

using var sourceImage = Image.Load(inputStream);
using var imageRgba32 = sourceImage.CloneAs<Rgba32>();
using var image = new ImageSharpImageSource(imageRgba32);

var asciiArt = generator.GenerateAsciiArtFromImage(image);

Console.WriteLine(asciiArt.Art);
