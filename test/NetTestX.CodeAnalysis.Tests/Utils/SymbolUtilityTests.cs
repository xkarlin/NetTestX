using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Utils;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Utils.Tests;

public class SymbolUtilityTests
{
    [Fact]
    public void TestGetDisplayName()
    {
        // Arrange
        var testSymbol = Substitute.For<ITypeSymbol>();
        testSymbol.ToDisplayString().Returns("Foo");

        // Act
        var result = SymbolUtility.GetDisplayName(testSymbol);

        // Assert
        Assert.Equal("Foo", result);
    }

    [Fact]
    public void TestGetMethodDisplayName()
    {
        // Arrange
        var testMethod = Substitute.For<IMethodSymbol>();
        testMethod.IsStatic.Returns(true);
        testMethod.IsAsync.Returns(true);
        testMethod.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Internal);
        testMethod.ContainingType.Returns(default(INamedTypeSymbol));
        testMethod.ToDisplayString(CommonFormats.NameOnlyGenericFormat).Returns("M");
        testMethod.ReturnType.ToDisplayString(CommonFormats.NameOnlyGenericFormat).Returns("T");
        testMethod.Parameters.Returns([]);

        // Act
        var result = SymbolUtility.GetDisplayName(testMethod);

        // Assert
        Assert.Equal("internal static async T M()", result);
    }

    [Fact]
    public void TestGetPropertyDisplayName()
    {
        // Arrange
        var testProperty = Substitute.For<IPropertySymbol>();
        testProperty.IsIndexer.Returns(true);
        testProperty.Type.ToDisplayString(CommonFormats.NameOnlyGenericFormat).Returns("T");
        testProperty.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Internal);
        testProperty.ContainingType.Returns(default(INamedTypeSymbol));
        testProperty.Parameters.Returns([]);

        // Act
        var result = SymbolUtility.GetDisplayName(testProperty);

        // Assert
        Assert.Equal("internal T this[]", result);
    }
}
