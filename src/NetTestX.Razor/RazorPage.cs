using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace NetTestX.Razor;

public abstract class RazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>, IRazorPage
{
    private readonly MemoryStream _outputStream = new();

    protected RazorPage()
    {
        HtmlEncoder = HtmlEncoder.Default;

        // ReSharper disable once VirtualMemberCallInConstructor
        ViewContext = new()
        {
            Writer = new StreamWriter(_outputStream)
        };
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
