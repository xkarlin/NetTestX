using System;
using System.Threading.Tasks;

namespace NetTestX.CodeAnalysis.Razor;

internal class RazorFileTemplate(object model)
{
    public IRazorPage Page { get; } = RazorPageLocator.FindPage(model.GetType());

    public object Model { get; } = model;

    public async Task<string> RenderAsync()
    {
        await Page.ExecuteAsync();
        return Page.GetRenderedString();
    }
}
