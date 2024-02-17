using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetTestX.VSIX.Options;

internal partial class OptionsProvider
{
    [ComVisible(true)]
    public class AdvancedOptionsPage : BaseOptionPage<AdvancedOptions> { }
}

public class AdvancedOptions : BaseOptionModel<AdvancedOptions>
{
    [Category("Advanced")]
    [DisplayName("Multiple Type Warning")]
    [Description("Whether to show the confirmation window when there are multiple types in the file available for generation.")]
    [DefaultValue(true)]
    public bool ShowMultipleTypeWarning { get; set; } = true;
}
