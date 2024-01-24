using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.VSIX.Code;

public interface IUnitTestGeneratorOptionsProvider
{
    UnitTestGeneratorOptions GetOptions(CodeProject project, INamedTypeSymbol type);
}
