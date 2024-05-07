using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.ConstructorResolvers.Tests;

public class DummyConstructorResolverTests
{
    [Fact]
    public void TestResolve()
    {
        // Arrange
        var accessibility = Microsoft.CodeAnalysis.Accessibility.Public;
        DummyConstructorResolver sut = new(accessibility);

        var testCtor = Substitute.For<IMethodSymbol>();
        testCtor.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Public);

        var testType = Substitute.For<INamedTypeSymbol>();
        testType.Constructors.Returns([testCtor]);

        // Act
        var result = sut.Resolve(testType);

        // Assert
        Assert.Equal(testCtor, result);
    }
}
