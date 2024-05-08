using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Utils;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Utils.Tests;

public class TypeSymbolEnumerableVisitorTests
{
    [Fact]
    public void TestVisitNamespace()
    {
        // Arrange
        TypeSymbolEnumerableVisitor sut = new();

        var testType1 = Substitute.For<INamedTypeSymbol>();
        testType1.Accept(sut).Returns([testType1]);

        var testType2 = Substitute.For<INamedTypeSymbol>();
        testType2.Accept(sut).Returns([testType2]);

        var testSymbol = Substitute.For<INamespaceSymbol>();
        testSymbol.GetMembers().Returns([testType1, testType2]);

        // Act
        var result = sut.VisitNamespace(testSymbol);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal([testType1, testType2], result);
    }

    [Fact]
    public void TestVisitNamedType()
    {
        // Arrange
        TypeSymbolEnumerableVisitor sut = new();

        var testSymbol = Substitute.For<INamedTypeSymbol>();

        // Act
        var result = sut.VisitNamedType(testSymbol);

        // Assert
        Assert.Single(result);
        Assert.Equal([testSymbol], result);
    }
}
