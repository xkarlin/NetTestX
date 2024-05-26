using System.Collections.Generic;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Extensions;

/// <summary>
/// Extensions for <see cref="CodeProject"/>
/// </summary>
public static class CodeProjectExtensions
{
    private const string PACKAGE_REFERENCE_ITEM_NAME = "PackageReference";

    /// <summary>
    /// Get all package references that this <see cref="CodeProject"/> contains
    /// </summary>
    public static IEnumerable<CodeProjectItem> GetPackageReferences(this CodeProject project) => project.GetItems(PACKAGE_REFERENCE_ITEM_NAME);

    /// <summary>
    /// Whether this <see cref="CodeProject"/> is a test project
    /// </summary>
    public static bool IsTestProject(this CodeProject project) => project.GetPropertyValue("IsTestProject") == "true";

    /// <summary>
    /// Get a <see cref="TestFramework"/> used by this <see cref="CodeProject"/>
    /// </summary>
    public static TestFramework GetProjectTestFramework(this CodeProject project) => TestFrameworkProjectLocator.GetTestFramework(project);

    /// <summary>
    /// Get a <see cref="MockingLibrary"/> used by this <see cref="CodeProject"/>
    /// </summary>
    public static MockingLibrary GetProjectMockingLibrary(this CodeProject project) => MockingLibraryProjectLocator.GetMockingLibrary(project);
}
