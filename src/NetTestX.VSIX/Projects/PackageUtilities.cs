using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.ComponentModelHost;
using NetTestX.CodeAnalysis.Workspaces.Generation;
using NuGet.VisualStudio;
using System;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Projects;

internal class PackageUtilities
{
    public static async Task InstallPackageAsync(DTEProject currentProject, PackageReference reference)
    {
        var componentModel = await VS.GetServiceAsync<SComponentModel, IComponentModel>();

        if (componentModel == null)
            throw new InvalidOperationException();
        
        var packageInstaller = componentModel.GetService<IVsPackageInstaller>();

        try
        {
            packageInstaller.InstallPackage("All", currentProject, reference.Name, "*", false);
        }
        catch (InvalidOperationException)
        {
            packageInstaller.InstallPackage("https://www.nuget.org/api/v2/", currentProject, reference.Name, default(string), false);
        }
    }
}
