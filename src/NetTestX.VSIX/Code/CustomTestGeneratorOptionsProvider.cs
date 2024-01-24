using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.VSIX.Code;

public sealed class CustomTestGeneratorOptionsProvider(string testClassName, string testClassNamespace) : DefaultTestGeneratorOptionsProvider
{
    public override UnitTestGeneratorOptions GetOptions(CodeProject project, INamedTypeSymbol type) => base.GetOptions(project, type) with
    {
        TestClassName = testClassName,
        TestClassNamespace = testClassNamespace
    };
}
