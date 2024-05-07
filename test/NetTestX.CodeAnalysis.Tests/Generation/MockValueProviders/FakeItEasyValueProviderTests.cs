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

public class FakeItEasyValueProviderTests
{
    [Fact]
    public void TestResolve()
    {
        // Arrange
        FakeItEasyValueProvider sut = new();

        var testType = Substitute.For<ITypeSymbol>();
        testType.TypeKind.Returns(TypeKind.Interface);
        testType.ToDisplayString(CommonFormats.ShortNullableFormat).Returns("ITest");

        // Act
        var result = sut.Resolve(testType);

        // Assert
        Assert.Equal("A.Fake<ITest>()", result);
    }

    [Fact]
    public void TestCollectNamespaces()
    {
        // Arrange
        FakeItEasyValueProvider sut = new();


        // Act
        var result = sut.CollectNamespaces();

        // Assert
        Assert.Equal(["FakeItEasy"], result);
    }
}
