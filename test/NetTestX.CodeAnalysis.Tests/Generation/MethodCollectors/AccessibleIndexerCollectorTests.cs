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

public class AccessibleIndexerCollectorTests
{
    [Fact]
    public void TestGetExcludedSymbols()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
public class C
{
    public int this[string x] => 42;
}
""");

        AccessibleIndexerCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };

        // Act
        var result = sut.GetExcludedSymbols(testContext);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void TestShouldCollectSymbol()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
public class C
{
    public int this[string x] => 42;
}
""");

        AccessibleIndexerCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };
        
        var testSymbol = testContext.Type.GetMembers().OfType<IPropertySymbol>().Where(x => x.IsIndexer).First();

        // Act
        var result = sut.ShouldCollectSymbol(testContext, testSymbol);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestCollectSymbol()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
public class C
{
    public int this[string x] => 42;
}
""");

        AccessibleIndexerCollector sut = new();

        var testContext = new MethodCollectionContext
        {
            Compilation = compilation,
            Type = compilation.GetTypeByMetadataName("C")
        };

        var testSymbol = testContext.Type.GetMembers().OfType<IPropertySymbol>().Where(x => x.IsIndexer).First();

        // Act
        var result = sut.CollectSymbol(testContext, testSymbol);

        // Assert
        Assert.Equal("TestStringIndexer", result.MethodName);
        Assert.Equal(testSymbol, result.Symbol);
        Assert.IsType<AccessibleIndexerMethodBodyModel>(result.MethodBodyModel);
    }
}
