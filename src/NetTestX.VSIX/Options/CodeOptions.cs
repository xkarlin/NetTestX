using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetTestX.VSIX.Options;

internal partial class OptionsProvider
{
    [ComVisible(true)]
    public class CodeOptionsPage : BaseOptionPage<CodeOptions> { }
}

public class CodeOptions : BaseOptionModel<CodeOptions>
{
    [Category("Code")]
    [DisplayName("Include AAA Comments")]
    [Description("Whether to include comments for AAA (Arrange / Act / Assert) sections in test methods.")]
    [DefaultValue(true)]
    public bool IncludeAAAComments { get; set; } = true;

    [Category("Code")]
    [DisplayName("Use Smart Generics")]
    [Description("Whether to automatically substitute type parameters for generic symbols. " +
                 "When disabled, generic symbols will remain open. \n" +
                 "NOTE: this feature is experimental and does not work for certain scenarios.")]
    [DefaultValue(false)]
    public bool UseSmartGenerics { get; set; } = false;
}
