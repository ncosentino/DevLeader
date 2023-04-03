using Autofac;

ContainerBuilder containerBuilder = new();

// register dependencies
containerBuilder.RegisterModule<MyModule>();

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

// get instance!
var urlSaver = scope.Resolve<AwesomeUrlSaver>();

await urlSaver.SaveUrlsAsync(
    "https://www.devleader.ca",
    "urls.txt");

public sealed class MyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AwesomeUrlSaver>()
            .SingleInstance();
        builder
            .RegisterType<UrlNormalizer>()
            .SingleInstance();
        builder
            .RegisterType<HtmlContentDownloader>()
            .SingleInstance();
        builder
            .RegisterType<HtmlUrlExtractor>()
            .SingleInstance();
        builder
            .RegisterType<UrlListOutputFormatter>()
            .SingleInstance();
        builder
            .RegisterType<UrlContentFileWriter>()
            .SingleInstance();
    }
}

public sealed class SomeComposedClass
{
    private readonly AwesomeUrlSaver _urlSaver;

    public SomeComposedClass(AwesomeUrlSaver urlSaver)
    {
        _urlSaver = urlSaver;
    }

    public void DoStuff()
    {
        // TODO: do some stuff!
    }
}

public sealed class SomeComposedClass2
{
    private readonly AwesomeUrlSaver _urlSaver;

    public SomeComposedClass2(AwesomeUrlSaver urlSaver)
    {
        _urlSaver = urlSaver;
    }

    public void DoStuff()
    {
        // TODO: do some stuff!
    }
}