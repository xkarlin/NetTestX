namespace NetTestX.Razor;

/// <summary>
/// Represents a razor page, capable of being materialized into a <c>string</c>
/// </summary>
public interface IRazorPage : Microsoft.AspNetCore.Mvc.Razor.IRazorPage
{
    void SetModel(object model);

    string GetRenderedString();
}
