using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis.Generation;

public class UnitTestGeneratorDriver(UnitTestGeneratorContext context)
{
    public async Task<string> GenerateTestClassSourceAsync()
    {
        TestClassModel model = new()
        {
            TestClassName = context.Options.TestClassName,
            TestClassNamespace = context.Options.TestClassNamespace
        };

        RazorFileTemplate template = new(model);

        string sourceText = await template.RenderAsync();
        return sourceText;
    }
}
