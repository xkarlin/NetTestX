using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation;

/// <summary>
/// Helper class for namespaces
/// </summary>
public static class NamespaceHelper
{
    /// <summary>
    /// A list of default namespaces used when scaffolding C# source files
    /// </summary>
    public static readonly IEnumerable<string> DefaultNamespaces =
    [
        "System",
        "System.Collections.Generic",
        "System.Linq",
        "System.Text",
        "System.Threading.Tasks",
    ];
}
