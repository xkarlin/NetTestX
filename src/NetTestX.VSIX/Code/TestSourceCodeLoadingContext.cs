using EnvDTE;
using EnvDTE80;

namespace NetTestX.VSIX.Code;

public readonly struct TestSourceCodeLoadingContext
{
    public required DTE2 DTE { get; init; }

    public required UIHierarchyItem[] SelectedItems { get; init; }
}
