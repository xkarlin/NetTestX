using System;

namespace NetTestX.CodeAnalysis;

/// <summary>
/// Advanced options (flags) passed to <see cref="UnitTestGeneratorDriver"/>
/// </summary>
[Flags]
public enum AdvancedGeneratorOptions
{
    /// <summary>
    /// No specific options
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates that the generated tests should contain AAA (Arrange - Act - Assert) comments
    /// </summary>
    IncludeAAAComments = 1 << 0,

    /// <summary>
    /// Indicates that the generated tests should use Smart Generics
    /// </summary>
    UseSmartGenerics = 1 << 1,

    /// <summary>
    /// Indicates that the tests should be generated for <c>internal</c> members too
    /// </summary>
    IncludeInternalMembers = 1 << 2
}
