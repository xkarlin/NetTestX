using NetTestX.CodeAnalysis;
using NetTestX.VSIX.Options;

namespace NetTestX.VSIX.Extensions;

public static class OptionsExtensions
{
    public static AdvancedGeneratorOptions GetAdvancedGeneratorOptions(this CodeOptions options)
    {
        var flags = AdvancedGeneratorOptions.None;

        if (options.IncludeAAAComments)
            flags |= AdvancedGeneratorOptions.IncludeAAAComments;

        return flags;
    }
}
