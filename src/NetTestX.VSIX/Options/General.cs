using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetTestX.VSIX.Options;

internal partial class OptionsProvider
{
    [ComVisible(true)]
    public class GeneralOptions : BaseOptionPage<General> { }
}

public class General : BaseOptionModel<General>
{
    [Category("General")]
    [DisplayName("Test File Name")]
    [Description("The file name of the test class.")]
    [DefaultValue("{TypeName}Tests")]
    public string TestFileName { get; set; } = "{TypeName}Tests";

    [Category("General")]
    [DisplayName("Test Class Name")]
    [Description("The name of the test class.")]
    [DefaultValue("{TypeName}Tests")]
    public string TestClassName { get; set; } = "{TypeName}Tests";

    [Category("General")]
    [DisplayName("Test Class Namespace")]
    [Description("The namespace of the test class.")]
    [DefaultValue("{TypeNamespace}.Tests")]
    public string TestClassNamespace { get; set; } = "{TypeNamespace}.Tests";
}
