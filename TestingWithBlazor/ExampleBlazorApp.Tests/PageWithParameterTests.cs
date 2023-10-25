using System.Reflection;

using Bunit;

using Castle.Core.Internal;

using ExampleBlazorApp.Pages;

using Microsoft.AspNetCore.Components;

using Xunit;

namespace ExampleBlazorApp.Tests;

public class PageWithParameterTests : TestContext
{
    [Fact]
    public void PageDeclaration_MatchesParameterDeclaration()
    {
        const string ParameterName = "TheParameter";

        var pageAttributes = typeof(PageWithParameter).GetCustomAttributes(true);
        var pageRouteAttribute = (RouteAttribute)Assert.Single(
            pageAttributes,
            x =>
                x is RouteAttribute routeAttribute &&
                routeAttribute.Template == $"/pagewithparam/{{{ParameterName}}}");

        var parameterNameProperty = Assert.Single(
            typeof(PageWithParameter).GetProperties(),
            p => p.Name == ParameterName);
        var parameterAttribute = Assert.Single(
            parameterNameProperty.CustomAttributes,
            x => x.AttributeType == typeof(ParameterAttribute));
    }
}