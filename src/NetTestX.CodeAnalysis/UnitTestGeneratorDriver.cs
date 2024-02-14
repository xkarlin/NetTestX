using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis;

public partial class UnitTestGeneratorDriver
{
    private readonly UnitTestGeneratorContext _context;

    private UnitTestGeneratorDriver(UnitTestGeneratorContext context)
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

        var resolvedType = SymbolGenerationResolver.Resolve(_context.Type, _context.Compilation);

        TestClassModel model = new(
            _context.Options.TestClassName,
            _context.Options.TestClassNamespace,
            resolvedType,
            testValueProvider,
            frameworkModel,
            _context.Options.TestMethods);

        return model;
    }
}
