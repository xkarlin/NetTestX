using System.Collections.Generic;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions;

public static class CodeProjectExtensions
{
    private const string PACKAGE_REFERENCE_ITEM_NAME = "PackageReference";

    public static IEnumerable<CodeProjectItem> GetPackageReferences(this CodeProject project) => project.GetItems(PACKAGE_REFERENCE_ITEM_NAME);

    public static bool IsTestProject(this CodeProject project) => project.GetPropertyValue("IsTestProject") == "true";

    public static TestFramework GetProjectTestFramework(this CodeProject project) => TestFrameworkProjectLocator.GetTestFramework(project);

    public static MockingLibrary GetProjectMockingLibrary(this CodeProject project) => MockingLibraryProjectLocator.GetMockingLibrary(project);
}
