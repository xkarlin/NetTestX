using NetTestX.CodeAnalysis.Common;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

public static class MockValueProviderLocator
{
    private static readonly Dictionary<MockingLibrary, Type> _typeCache = [];

    static MockValueProviderLocator()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            OnAssemblyLoaded(assembly);

        AppDomain.CurrentDomain.AssemblyLoad += (_, args) => OnAssemblyLoaded(args.LoadedAssembly);
    }

    public static IMockValueProvider LocateValueProvider(MockingLibrary library)
    {
        if (!_typeCache.TryGetValue(library, out var pageType))
            throw new InvalidOperationException($"Could not find a value provider for {library}");

        var provider = (IMockValueProvider)Activator.CreateInstance(pageType);
        return provider;
    }

    private static void OnAssemblyLoaded(Assembly assembly)
    {
        if (!assembly.FullName.StartsWith("NetTestX"))
            return;

        foreach (var assemblyType in assembly.GetTypes())
        {
            if (assemblyType.GetCustomAttribute<MockValueProviderAttribute>() is { } attribute)
                _typeCache[attribute.Library] = assemblyType;
        }
    }
}
