namespace NetTestX.CodeAnalysis.Razor;

public interface IRazorPage : Microsoft.AspNetCore.Mvc.Razor.IRazorPage
{
    void SetModel(object model);

    string GetRenderedString();
}
