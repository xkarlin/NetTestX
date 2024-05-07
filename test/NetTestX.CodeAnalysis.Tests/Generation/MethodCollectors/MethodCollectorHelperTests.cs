using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis;
using NetTestX.Common.Diagnostics;
using NetTestX.CodeAnalysis.Tests;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors.Tests;

public class MethodCollectorHelperTests
{
    [Fact]
    public void TestGetAvailableCollectors()
    {
        // Arrange

        // Act
        var result = MethodCollectorHelper.GetAvailableCollectors();

        // Assert
        Assert.Equal(5, result.Count());
        Assert.True(
        new Type[]
        {
            typeof(AccessibleInstanceMethodCollector),
            typeof(AccessibleStaticMethodCollector),
            typeof(AccessibleIndexerCollector),
            typeof(DisposableTypeCollector),
            typeof(AsyncDisposableTypeCollector)
        }.SequenceEqual(result.Select(x => x.GetType())));
    }

    [Fact]
    public void TestCollectTestMethods()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;
using System.Threading.Tasks;

public class C : IDisposable, IAsyncDisposable
{
    public void M1() { }

    internal void M2() { }

    public int this[string x] => 42;

    internal static void M3() { }

    void IDisposable.Dispose() { }

    ValueTask IAsyncDisposable.DisposeAsync() => default;
}
""");

        var testType = compilation.GetTypeByMetadataName("C");
        var testCompilation = compilation;
        var testAdvancedOptions = AdvancedGeneratorOptions.IncludeInternalMembers;
        var testReporter = Substitute.For<IDiagnosticReporter>();

        // Act
        var result = MethodCollectorHelper.CollectTestMethods(testType, testCompilation, testAdvancedOptions, testReporter);

        // Assert
        Assert.Equal(6, result.Count);
    }
}
