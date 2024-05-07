using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Common;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Common.Tests;

public class SymbolNameComparerTests
{
    [Fact]
    public void TestEquals()
    {
        // Arrange
        SymbolNameComparer sut = new();

        var testX = Substitute.For<ISymbol>();
        testX.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");

        var testY = Substitute.For<ISymbol>();
        testY.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");

        // Act
        var result = sut.Equals(testX, testY);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestGetHashCode()
    {
        // Arrange
        SymbolNameComparer sut = new();

        var testObj = Substitute.For<ISymbol>();
        testObj.Name.Returns("Test");

        // Act
        var result = sut.GetHashCode(testObj);

        // Assert
        Assert.Equal("Test".GetHashCode(), result);
    }
}
