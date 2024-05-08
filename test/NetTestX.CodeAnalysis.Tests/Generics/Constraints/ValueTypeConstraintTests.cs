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

public class ValueTypeConstraintTests
{
    [Fact]
    public void TestIsSatisfiedBy()
    {
        // Arrange
        ValueTypeConstraint sut = new();

        var testType = Substitute.For<ITypeSymbol>();
        testType.IsValueType.Returns(true);

        // Act
        var result = sut.IsSatisfiedBy(testType);

        // Assert
        Assert.True(result);
    }
}
