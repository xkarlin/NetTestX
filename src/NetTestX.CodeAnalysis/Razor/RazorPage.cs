using System.IO;
using System.Text;

namespace NetTestX.CodeAnalysis.Razor;

public abstract class RazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>, IRazorPage
{
    private readonly MemoryStream _outputStream = new();

    protected RazorPage()
    {
        ViewContext = new()
        {
            Writer = new StreamWriter(_outputStream)
        };
    }

    string IRazorPage.GetRenderedString()
    {
        _outputStream.Seek(0, SeekOrigin.Begin);
        string output = Encoding.UTF8.GetString(_outputStream.ToArray());
        return output;
    }
}
