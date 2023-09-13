using BlazorPluginsExample.PluginApi;

using Microsoft.AspNetCore.Components;

namespace BlazorPluginsExample.RenderFragmentPluginLibrary;

public sealed class RenderFragmentPlugin : IPluginApi3
{
    public Task<RenderFragment> GetRenderFragmentAsync()
        => Task.FromResult(new RenderFragment(builder =>
        {
            var content =
            """
            <div class='alert alert-primary' role='alert'>
                Woah!! This is from PluginApi3!
            """;

            builder.OpenElement(1, "p");
            builder.AddContent(2, new MarkupString(content));
            builder.CloseElement();
        }));
}
