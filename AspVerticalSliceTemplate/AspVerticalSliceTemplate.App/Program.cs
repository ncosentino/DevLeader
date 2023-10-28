// this template was first shared with my Dev Leader Weekly subscribers here:
// https://www.devleader.ca/2023/10/28/vertical-slice-template-dev-leader-weekly-15/
// if you'd like to subscribe to future newsletters, then you can visit here:
// https://subscribe.devleader.ca
// it's TOTALLY free and you'll get one email a week with some of my thoughts
// on leadership, software development, and dotnet!
using System.Reflection;

using Autofac;

ContainerBuilder containerBuilder = new();

// TODO: if you have plugin dependencies that are in different
// DLLs, then consider how you'll scan for those to pull
// them in!
containerBuilder.RegisterAssemblyModules(
    Assembly.GetExecutingAssembly());

using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();
var app = scope.Resolve<ConfiguredWebApplication>();
app.WebApplication.Run();