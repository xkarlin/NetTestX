using System;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class TestFrameworkAttribute(TestFramework framework) : Attribute
{
    public TestFramework Framework { get; } = framework;
}
