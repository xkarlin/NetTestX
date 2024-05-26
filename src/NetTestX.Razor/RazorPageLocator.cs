using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetTestX.Razor;

/// <summary>
/// Helper class used to locate <see cref="IRazorPage"/>s based on model types they use
/// </summary>
public static class RazorPageLocator
{
    private static readonly Dictionary<Type, Type> _typeCache = [];

    static RazorPageLocator()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            OnAssemblyLoaded(assembly);

        AppDomain.CurrentDomain.AssemblyLoad += (_, args) => OnAssemblyLoaded(args.LoadedAssembly);
    }

    /// <summary>
    /// Find an instance of <see cref="IRazorPage"/> for the given model <paramref name="type"/>
    /// </summary>
    public static IRazorPage FindPage(Type type)
    {
        if (!_typeCache.TryGetValue(type, out var pageType))
            throw new InvalidOperationException($"Could not find a razor view for a given model type {type}");

        var page = (IRazorPage)Activator.CreateInstance(pageType);
        return page;
    }

    private static void OnAssemblyLoaded(Assembly assembly)
    {
        if (!assembly.FullName.StartsWith("NetTestX"))
            return;

        foreach (var assemblyType in assembly.GetTypes())
        {
            if (assemblyType.BaseType is { IsGenericType: true } @base && @base.GetGenericTypeDefinition() == typeof(RazorPage<>))
            {
                var modelType = @base.GenericTypeArguments[0];
                _typeCache[modelType] = assemblyType;
            }
        }
    }
}
