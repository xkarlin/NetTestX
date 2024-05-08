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

public class ConstraintHelperTests
{
    [Fact]
    public void TestGetTypeParameterConstraints()
    {
        // Arrange
        var testConstraintType = Substitute.For<INamedTypeSymbol>();

        var testTypeParameter = Substitute.For<ITypeParameterSymbol>();
        testTypeParameter.HasConstructorConstraint.Returns(true);
        testTypeParameter.HasReferenceTypeConstraint.Returns(true);
        testTypeParameter.HasUnmanagedTypeConstraint.Returns(false);
        testTypeParameter.HasValueTypeConstraint.Returns(false);
        testTypeParameter.ConstraintTypes.Returns([testConstraintType]);

        // Act
        var result = ConstraintHelper.GetTypeParameterConstraints(testTypeParameter);

        // Assert
        Assert.Equal(3, result.Count());
        Assert.False(new[]
        {
            typeof(TypeConstraint),
            typeof(ConstructorConstraint),
            typeof(ReferenceTypeConstraint)
        }.Except(result.Select(x => x.GetType())).Any());
    }

    [Fact]
    public void TestGetTypeParameterConstraintsWithoutTypes()
    {
        // Arrange
        var testConstraintType = Substitute.For<INamedTypeSymbol>();

        var testTypeParameter = Substitute.For<ITypeParameterSymbol>();
        testTypeParameter.HasConstructorConstraint.Returns(true);
        testTypeParameter.HasReferenceTypeConstraint.Returns(true);
        testTypeParameter.HasUnmanagedTypeConstraint.Returns(false);
        testTypeParameter.HasValueTypeConstraint.Returns(false);
        testTypeParameter.ConstraintTypes.Returns([testConstraintType]);

        // Act
        var result = ConstraintHelper.GetTypeParameterConstraintsWithoutTypes(testTypeParameter);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.False(new[] { typeof(ConstructorConstraint), typeof(ReferenceTypeConstraint) }.Except(result.Select(x => x.GetType())).Any());
    }
}
