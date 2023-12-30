using System;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Razor;

internal static class RazorPageLocator
{
    private static readonly Dictionary<Type, Type> _typeCache = [];

    public static IRazorPage FindPage(Type type)
    {
        if (!_typeCache.TryGetValue(type, out var pageType))
        {
            var baseType = typeof(RazorPage<>).MakeGenericType(type);

            foreach (var assemblyType in typeof(RazorPage<>).Assembly.GetTypes())
            {
                if (assemblyType.BaseType == baseType)
                {
                    pageType = assemblyType;
                    _typeCache[type] = pageType;
                    break;
                }
            }
        }

        if (pageType is null)
            throw new InvalidOperationException($"Could not find a razor view for a given model type {type}");

        var page = (IRazorPage)Activator.CreateInstance(pageType);
        return page;
    }
}
