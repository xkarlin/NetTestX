using System.Threading.Tasks;

namespace NetTestX.Razor;

/// <summary>
/// Template for a Razor file
/// </summary>
public class RazorFileTemplate
{
    /// <summary>
    /// The Razor page that this file contains
    /// </summary>
    public IRazorPage Page { get; }

    /// <summary>
    /// The model used by the <see cref="Page"/>
    /// </summary>
    public object Model { get; }

    /// <summary>
    /// Create a file template using the provided <paramref name="model"/>
    /// </summary>
    public RazorFileTemplate(object model)
    {
        Page = RazorPageLocator.FindPage(model.GetType());
        Model = model;

        Page.SetModel(Model);
    }

    /// <summary>
    /// Render this <see cref="Page"/> into a string
    /// </summary>
    public async Task<string> RenderAsync()
    {
        await Page.ExecuteAsync();
        return Page.GetRenderedString();
    }
}
