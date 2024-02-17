using System;

namespace NetTestX.CodeAnalysis;

[Flags]
public enum AdvancedGeneratorOptions
{
    None = 0,

    IncludeAAAComments = 1 << 0
}
