using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generics.Constraints;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generics.Constraints.Tests;

public class ConstructorConstraintTests
{
    [Fact]
    public void TestIsSatisfiedBy()
    {
        // Arrange
        ConstructorConstraint sut = new();

        var testCtor = Substitute.For<IMethodSymbol>();
        testCtor.Parameters.Returns([]);
        testCtor.IsStatic.Returns(false);
        testCtor.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Public);

        var testType = Substitute.For<INamedTypeSymbol>();
        testType.Constructors.Returns([testCtor]);
        testType.Kind.Returns(SymbolKind.NamedType);

        // Act
        var result = sut.IsSatisfiedBy(testType);

        // Assert
        Assert.True(result);
    }
}
