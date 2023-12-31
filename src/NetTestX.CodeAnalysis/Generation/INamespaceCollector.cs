using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation;

public interface INamespaceCollector
{
    IEnumerable<string> CollectNamespaces();
}
