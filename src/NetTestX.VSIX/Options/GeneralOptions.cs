using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetTestX.VSIX.Options;

internal partial class OptionsProvider
{
    [ComVisible(true)]
    public class GeneralOptionsPage : BaseOptionPage<GeneralOptions> { }
}

/// <summary>
/// General options contained in the "General" options page inside VS settings
/// </summary>
public class GeneralOptions : BaseOptionModel<GeneralOptions>
{
    [Category("General")]
    [DisplayName("Test Internal Members")]
    [Description("Whether to test internal members of types.")]
    [DefaultValue(false)]
    public bool TestInternalMembers { get; set; } = false;

    [Category("Naming")]
    [DisplayName("Test File Name")]
    [Description("The file name of the test class.")]
    [DefaultValue("{TypeName}Tests")]
    public string TestFileName { get; set; } = "{TypeName}Tests";

    [Category("Naming")]
    [DisplayName("Test Class Name")]
    [Description("The name of the test class.")]
    [DefaultValue("{TypeName}Tests")]
    public string TestClassName { get; set; } = "{TypeName}Tests";

    [Category("Naming")]
    [DisplayName("Test Class Namespace")]
    [Description("The namespace of the test class.")]
    [DefaultValue("{TypeNamespace}.Tests")]
    public string TestClassNamespace { get; set; } = "{TypeNamespace}.Tests";
}
