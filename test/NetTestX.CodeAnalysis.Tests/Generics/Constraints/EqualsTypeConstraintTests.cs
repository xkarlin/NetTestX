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

public class EqualsTypeConstraintTests
{
    [Fact]
    public void TestIsSatisfiedBy()
    {
        // Arrange
        var expectedType = Substitute.For<ITypeSymbol>();
        EqualsTypeConstraint sut = new(expectedType);

        // Act
        var result = sut.IsSatisfiedBy(expectedType);

        // Assert
        Assert.True(result);
    }
}
