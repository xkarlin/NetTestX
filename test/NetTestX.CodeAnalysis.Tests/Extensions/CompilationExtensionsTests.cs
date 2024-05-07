using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Extensions;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Tests;

namespace NetTestX.CodeAnalysis.Extensions.Tests;

public class CompilationExtensionsTests
{
    [Fact]
    public void TestGetNamedSymbolGeneric()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation("");

        // Act
        var result = CompilationExtensions.GetNamedSymbol<string>(testCompilation);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.SpecialType == SpecialType.System_String);
    }

    [Fact]
    public void TestGetNamedSymbol()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation("");
        var testType = typeof(char);

        // Act
        var result = CompilationExtensions.GetNamedSymbol(testCompilation, testType);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.SpecialType == SpecialType.System_Char);
    }

    [Fact]
    public void TestGetTypeSymbolGeneric()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation("");

        // Act
        var result = CompilationExtensions.GetTypeSymbol<float>(testCompilation);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.SpecialType == SpecialType.System_Single);
    }

    [Fact]
    public void TestGetTypeSymbol()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation("");
        var testType = typeof(double);

        // Act
        var result = CompilationExtensions.GetTypeSymbol(testCompilation, testType);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.SpecialType == SpecialType.System_Double);
    }
}
