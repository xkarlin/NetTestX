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

public class TypeConstraintTests
{
    [Fact]
    public void TestIsSatisfiedBy()
    {
        // Arrange
        var constraintType = Substitute.For<INamedTypeSymbol>();
        constraintType.TypeKind.Returns(TypeKind.Interface);

        TypeConstraint sut = new(constraintType);

        var testType = Substitute.For<ITypeSymbol>();
        testType.AllInterfaces.Returns([constraintType]);

        // Act
        var result = sut.IsSatisfiedBy(testType);

        // Assert
        Assert.True(result);
    }
}
