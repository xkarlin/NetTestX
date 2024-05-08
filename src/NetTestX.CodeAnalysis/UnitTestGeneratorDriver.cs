using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis;

public partial class UnitTestGeneratorDriver
{
    private readonly UnitTestGeneratorContext _context;

    internal UnitTestGeneratorContext Context => _context;

    internal UnitTestGeneratorDriver(UnitTestGeneratorContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateTestClassSourceAsync()
    {
        var model = CreateTestClassModel();
        RazorFileTemplate template = new(model);

        string sourceText = await template.RenderAsync();
        return sourceText;
    }

    private TestClassModel CreateTestClassModel()
    {
        var testValueProvider = MockValueProviderLocator.LocateValueProvider(_context.Options.MockingLibrary);
        var frameworkModel = TestFrameworkModelLocator.LocateModel(_context.Options.TestFramework);

        TestClassModel model = new(_context.TestMethods)
        {
            TestClassName = _context.Options.TestClassName,
            TestClassNamespace = _context.Options.TestClassNamespace,
            Type = _context.Type,
            ValueProvider = testValueProvider,
            TestFrameworkModel = frameworkModel,
            AdvancedOptions = _context.Options.AdvancedOptions
        };

        return model;
    }
}
