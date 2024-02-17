using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetTestX.VSIX.Options;

internal partial class OptionsProvider
{
    [ComVisible(true)]
    public class AdvancedOptions : BaseOptionPage<Advanced> { }
}

public class Advanced : BaseOptionModel<Advanced>
{
    [Category("Advanced")]
    [DisplayName("Include AAA Comments")]
    [Description("Whether to include comments for AAA (Arrange / Act / Assert) sections in test methods.")]
    [DefaultValue(true)]
    public bool IncludeAAAComments { get; set; } = true;

    [Category("Advanced")]
    [DisplayName("Multiple Type Warning")]
    [Description("Whether to show the confirmation window when there are multiple types in the file available for generation.")]
    [DefaultValue(true)]
    public bool ShowMultipleTypeWarning { get; set; } = true;
}
