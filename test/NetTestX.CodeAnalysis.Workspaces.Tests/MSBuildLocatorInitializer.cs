using Microsoft.Build.Locator;
using System.Runtime.CompilerServices;

namespace NetTestX.CodeAnalysis.Workspaces.Tests;

internal class MSBuildLocatorInitializer
{
    [ModuleInitializer]
    public static void RegisterLocatorDefaults()
    {
        MSBuildLocator.RegisterDefaults();
    }
}
