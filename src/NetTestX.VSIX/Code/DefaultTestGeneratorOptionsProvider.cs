using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.VSIX.Code;

public class DefaultTestGeneratorOptionsProvider : IUnitTestGeneratorOptionsProvider
{
    public virtual UnitTestGeneratorOptions GetOptions(CodeProject project, INamedTypeSymbol type) => new()
    {
        TestClassName = $"{type.Name}Tests",
        TestClassNamespace = $"{type.ContainingNamespace}.Tests",
        TestFramework = project.GetProjectTestFramework(),
        MockingLibrary = project.GetProjectMockingLibrary()
    };
}
