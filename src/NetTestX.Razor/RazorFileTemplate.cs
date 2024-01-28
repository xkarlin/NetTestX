using System.Threading.Tasks;

namespace NetTestX.Razor;

public class RazorFileTemplate
{
    public IRazorPage Page { get; }

    public object Model { get; }

    public RazorFileTemplate(object model)
    {
        Page = RazorPageLocator.FindPage(model.GetType());
        Model = model;

        Page.SetModel(Model);
    }

    public async Task<string> RenderAsync()
    {
        await Page.ExecuteAsync();
        return Page.GetRenderedString();
    }
}
