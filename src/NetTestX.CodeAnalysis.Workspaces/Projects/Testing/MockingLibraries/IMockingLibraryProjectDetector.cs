using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

internal interface IMockingLibraryProjectDetector
{
    MockingLibrary MockingLibrary { get; }

    bool Detect(CodeProject project);
}
