using System.Text;

namespace AsciiArtGenerator.Steps.Step2;

internal sealed class ImageSharpAsciiArtGenerator
{
    public AsciiArt GenerateAsciiArtFromImage(Stream inputStream)
    {
        var asciiChars = "@%#*+=-:,. ";

        using var sourceImage = Image.Load(inputStream);
        using var image = sourceImage.CloneAs<Rgba32>();

        var aspect = image.Width / (double)image.Height;
        var outputWidth = image.Width / 20;
        var widthStep = image.Width / outputWidth;
        var outputHeight = (int)(outputWidth / aspect);
        var heightStep = image.Height / outputHeight;

        StringBuilder asciiBuilder = new(outputWidth * outputHeight);
        for (var h = 0; h < image.Height; h += heightStep)
        {
            for (var w = 0; w < image.Width; w += widthStep)
            {
                var pixelColor = image[w, h];
                var grayValue = 
                    (int)(pixelColor.R * 0.3 +
                    pixelColor.G * 0.59 +
                    pixelColor.B * 0.11);
                var asciiChar = asciiChars[
                    grayValue * (asciiChars.Length - 1) / 255];
                asciiBuilder.Append(asciiChar);
            }

            asciiBuilder.AppendLine();
        }

        AsciiArt art = new(
            asciiBuilder.ToString(),
            outputWidth,
            outputHeight);
        return art;
    }
}