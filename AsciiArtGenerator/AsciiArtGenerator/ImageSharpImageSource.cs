namespace AsciiArtGenerator;

internal sealed class ImageSharpImageSource : IImageSource
{
    private readonly Image<Rgba32> _image;

    public ImageSharpImageSource(Image<Rgba32> image)
    {
        _image = image;
    }

    public int Width => _image.Width;

    public int Height => _image.Height;

    public float AspectRatio => _image.Width / (float)_image.Height;

    public Rgb GetPixel(int x, int y)
    {
        var pixel = _image[x, y];
        return new(
            pixel.R,
            pixel.G,
            pixel.B);
    }

    public void Dispose() => _image.Dispose();
}