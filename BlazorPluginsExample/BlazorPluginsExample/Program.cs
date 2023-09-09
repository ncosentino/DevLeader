using System.Reflection;

using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder
    .Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb =>
    {
        foreach (var file in Directory.GetFiles(
            AppDomain.CurrentDomain.BaseDirectory,
            "*plugin*.dll", 
            SearchOption.TopDirectoryOnly))
        {
            var assembly = Assembly.LoadFrom(file);
            cb.RegisterAssemblyModules(assembly);
        }
    }))
    .ConfigureServices(services => services.AddAutofac());

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
