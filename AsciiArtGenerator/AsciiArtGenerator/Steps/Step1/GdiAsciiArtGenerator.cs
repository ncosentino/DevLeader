using System.Drawing;
using System.Text;

namespace AsciiArtGenerator.Steps.Step1;

internal sealed class GdiAsciiArtGenerator
{
    public string GenerateAsciiArtFromImage(
        Stream inputStream)
    {
        var asciiChars = "@%#*+=-:,. ";

        using var image = new Bitmap(inputStream);

        var outputWidth = image.Width / 20;
        var widthStep = image.Width / outputWidth;
        var outputHeight = image.Height / 20;
        var heightStep = image.Height / outputHeight;

        Console.WindowWidth = outputWidth;
        Console.WindowHeight = outputHeight;

        StringBuilder asciiBuilder = new(outputWidth * outputHeight);
        for (var h = 0; h < image.Height; h += heightStep)
        {
            for (var w = 0; w < image.Width; w += widthStep)
            {
                var pixelColor = image.GetPixel(w, h);
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

        return asciiBuilder.ToString();
    }
}