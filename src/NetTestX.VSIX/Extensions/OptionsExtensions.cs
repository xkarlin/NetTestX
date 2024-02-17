using NetTestX.CodeAnalysis;

namespace NetTestX.VSIX.Extensions;

public static class OptionsExtensions
{
    public static AdvancedGeneratorOptions GetAdvancedGeneratorOptions(this Advanced options)
    {
        var flags = AdvancedGeneratorOptions.None;

        if (options.IncludeAAAComments)
            flags |= AdvancedGeneratorOptions.IncludeAAAComments;

        return flags;
    }
}
