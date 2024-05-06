using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetTestX.Razor.Extensions;

public static class RazorPageExtensions
{
    public static async Task<string> ImportPagesAsync<T>(this RazorPage<T> page, IEnumerable<object> models, string separator = "")
    {
        StringBuilder builder = new();

        var enumerator = models.GetEnumerator();

        bool canMove = enumerator.MoveNext();

        while (canMove)
        {
            var item = enumerator.Current;

            string renderedPage = await page.ImportPageAsync(item);
            builder.Append(renderedPage);

            canMove = enumerator.MoveNext();

            if (canMove)
                builder.Append(separator);
        }
        
        return builder.ToString();
    }
}
