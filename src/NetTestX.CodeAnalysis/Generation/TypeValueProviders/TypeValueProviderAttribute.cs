using System;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class TypeValueProviderAttribute(MockingLibrary library) : Attribute
{
    public MockingLibrary Library { get; set; } = library;
}
