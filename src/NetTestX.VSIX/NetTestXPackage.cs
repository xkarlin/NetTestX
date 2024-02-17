using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Options;

namespace NetTestX.VSIX;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[Guid(PackageGuids.NetTestXString)]
[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasSingleProject_string, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasMultipleProjects_string, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideOptionPage(typeof(OptionsProvider.GeneralOptionsPage), "NetTestX", "General", 0, 0, true, SupportsProfiles = true)]
[ProvideOptionPage(typeof(OptionsProvider.CodeOptionsPage), "NetTestX", "Code", 0, 0, true, SupportsProfiles = true)]
[ProvideOptionPage(typeof(OptionsProvider.AdvancedOptionsPage), "NetTestX", "Advanced", 0, 0, true, SupportsProfiles = true)]
public sealed class NetTestXPackage : ToolkitPackage
{
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        await this.RegisterCommandsAsync();
    }
}
