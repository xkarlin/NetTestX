using NetTestX.CodeAnalysis.Workspaces.Generation;

namespace NetTestX.CodeAnalysis.Workspaces.Templates;

public class PackageReferenceModel(PackageReference reference)
{
    public PackageReference Reference { get; set; } = reference;
}
