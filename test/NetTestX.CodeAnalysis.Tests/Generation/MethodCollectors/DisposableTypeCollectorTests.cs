using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Tests;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors.Tests;

public class DisposableTypeCollectorTests
{
    [Fact]
    public void TestGetExcludedSymbols()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;

public class C : IDisposable
{
    public void Dispose() { }
}
""");

        DisposableTypeCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };

        // Act
        var result = sut.GetExcludedSymbols(testContext);

        // Assert
        Assert.Single(result);
        Assert.Equal("Dispose", result.First().Name);
    }

    [Fact]
    public void TestShouldCollectSymbol()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;

public class C : IDisposable
{
    public void Dispose() { }
}
""");

        DisposableTypeCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };

        // Act
        var result = sut.ShouldCollectSymbol(testContext, testContext.Type);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCollectSymbol()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;

public class C : IDisposable
{
    public void Dispose() { }
}
""");

        DisposableTypeCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };

        // Act
        var result = sut.CollectSymbol(testContext, testContext.Type);

        // Assert
        Assert.Equal("TestDisposable", result.MethodName);
        Assert.Equal(testContext.Type, result.Symbol);
        Assert.IsType<DisposableTypeMethodBodyModel>(result.MethodBodyModel);
    }
}
