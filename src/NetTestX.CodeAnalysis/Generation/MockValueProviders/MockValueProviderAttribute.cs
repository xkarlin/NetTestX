using System;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class MockValueProviderAttribute(MockingLibrary library) : Attribute
{
    public MockingLibrary Library { get; set; } = library;
}
