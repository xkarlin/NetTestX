namespace NetTestX.CodeAnalysis.Common;

/// <summary>
/// Helpers for source file outputs
/// </summary>
public static class SourceFileExtensions
{
    /// <summary>
    /// Constant value representing C# file extension (*.cs)
    /// </summary>
    public const string CSHARP = "cs";

    /// <summary>
    /// <inheritdoc cref="CSHARP"/> with the leading dot
    /// </summary>
    public const string CSHARP_DOT = $".{CSHARP}";
}
