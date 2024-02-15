using System;

namespace NetTestX.CodeAnalysis;

[Flags]
public enum AdvancedGeneratorOptions
{
    IncludeAAAComments = 1 << 0,

    Default = IncludeAAAComments
}
