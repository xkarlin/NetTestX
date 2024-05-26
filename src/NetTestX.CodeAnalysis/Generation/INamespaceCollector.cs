using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation;

/// <summary>
/// Represents an object that adds a number of namespaces at the top of the generated file
/// </summary>
public interface INamespaceCollector
{
    /// <summary>
    /// Collect all namespaces used by this object
    /// </summary>
    IEnumerable<string> CollectNamespaces();
}
