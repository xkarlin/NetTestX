using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public static class MethodCollectorLocator
{
    public static IEnumerable<ITestMethodCollector> GetAvailableCollectors()
    {
        foreach (var type in typeof(ITestMethodCollector).Assembly.DefinedTypes)
        {
            if (type.ImplementedInterfaces.Contains(typeof(ITestMethodCollector)))
            {
                var collector = (ITestMethodCollector)Activator.CreateInstance(type);
                yield return collector;
            }
        }
    }
}
