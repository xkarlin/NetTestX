using System;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public static class MethodCollectorLocator
{
    public static IEnumerable<ITestMethodCollector> GetAvailableCollectors() =>
    [
        new AccessibleInstanceMethodCollector(),
        new AccessibleStaticMethodCollector()
    ];
}
