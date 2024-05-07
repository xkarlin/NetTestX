using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders.Tests;

public class MoqValueProviderTests
{
    [Fact]
    public void TestResolve()
    {
        // Arrange
        MoqValueProvider sut = new();

        var testType = Substitute.For<ITypeSymbol>();
        testType.TypeKind.Returns(TypeKind.Interface);
        testType.ToDisplayString(CommonFormats.ShortNullableFormat).Returns("ITest");

        // Act
        var result = sut.Resolve(testType);

        // Assert
        Assert.Equal("new Mock<ITest>().Object", result);
    }

    [Fact]
    public void TestCollectNamespaces()
    {
        // Arrange
        MoqValueProvider sut = new();


        // Act
        var result = sut.CollectNamespaces();

        // Assert
        Assert.Equal(["Moq"], result);
    }
}
