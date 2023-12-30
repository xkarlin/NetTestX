namespace NetTestX.CodeAnalysis.Razor;

public interface IRazorPage : Microsoft.AspNetCore.Mvc.Razor.IRazorPage
{
    string GetRenderedString();
}
