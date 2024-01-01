using System.Collections.Generic;
using System.Reflection;
using System;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public static class TestFrameworkModelLocator
{
    private static readonly Dictionary<TestFramework, Type> _typeCache = [];

    static TestFrameworkModelLocator()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            OnAssemblyLoaded(assembly);

        AppDomain.CurrentDomain.AssemblyLoad += (_, args) => OnAssemblyLoaded(args.LoadedAssembly);
    }

    public static ITestFrameworkModel LocateModel(TestFramework framework)
    {
        if (!_typeCache.TryGetValue(framework, out var pageType))
            throw new InvalidOperationException($"Could not find a test framework model for {framework}");

        var model = (ITestFrameworkModel)Activator.CreateInstance(pageType);
        return model;
    }

    private static void OnAssemblyLoaded(Assembly assembly)
    {
        if (!assembly.FullName.StartsWith("NetTestX"))
            return;

        foreach (var assemblyType in assembly.GetTypes())
        {
            if (assemblyType.GetCustomAttribute<TestFrameworkAttribute>() is { } attribute)
                _typeCache[attribute.Framework] = assemblyType;
        }
    }
}
