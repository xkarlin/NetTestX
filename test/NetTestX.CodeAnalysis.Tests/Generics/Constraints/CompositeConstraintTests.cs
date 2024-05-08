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

public class CompositeConstraintTests
{
    [Fact]
    public void TestIsSatisfiedBy()
    {
        // Arrange
        var constraints = new IConstraint[] { new ValueTypeConstraint(), new UnmanagedTypeConstraint() };
        CompositeConstraint sut = new(constraints);

        var testType = Substitute.For<ITypeSymbol>();
        testType.IsUnmanagedType.Returns(true);
        testType.IsValueType.Returns(true);

        // Act
        var result = sut.IsSatisfiedBy(testType);

        // Assert
        Assert.True(result);
    }
}
