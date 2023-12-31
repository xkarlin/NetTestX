using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetTestX.Razor.Utils;

namespace NetTestX.Razor;

public abstract class RazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>, IRazorPage
{
    private readonly MemoryStream _outputStream = new();

    private string _lastIndentation;

    protected RazorPage()
    {
        HtmlEncoder = NullHtmlEncoder.Default;

        // ReSharper disable once VirtualMemberCallInConstructor
        ViewContext = new()
        {
            Writer = new StreamWriter(_outputStream)
        };
    }

    public async Task<string> ImportPageAsync(object model)
    {
        RazorFileTemplate nestedTemplate = new(model);
        string nestedText = await nestedTemplate.RenderAsync();
        return RazorRenderUtilities.NormalizeIndentation(nestedText, _lastIndentation);
    }

    public override void WriteLiteral(string value)
    {
        int indentationLength = value.AsSpan().Length - value.AsSpan().TrimEnd(' ').Length;
        _lastIndentation = value[(value.Length - indentationLength)..];

        base.WriteLiteral(value);
    }

    void IRazorPage.SetModel(object model)
    {
        DefaultCompositeMetadataDetailsProvider detailsProvider = new(Array.Empty<IMetadataDetailsProvider>());
        DefaultModelMetadataProvider modelProvider = new(detailsProvider);
        ModelStateDictionary dictionary = new();
        
        ViewData = new(new(modelProvider, dictionary), model);
    }

    string IRazorPage.GetRenderedString()
    {
        Output.Flush();

        _outputStream.Seek(0, SeekOrigin.Begin);
        string output = Encoding.UTF8.GetString(_outputStream.ToArray());
        return output;
    }
}
