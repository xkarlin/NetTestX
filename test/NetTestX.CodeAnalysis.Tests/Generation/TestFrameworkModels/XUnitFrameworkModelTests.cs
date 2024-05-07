using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NSubstitute;
using Xunit;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels.Tests;

public class XUnitFrameworkModelTests
{
    [Fact]
    public void TestAssertTrue()
    {
        // Arrange
        XUnitFrameworkModel sut = new();

        var testExpression = "2 + 2 == 4";

        // Act
        var result = sut.AssertTrue(testExpression);

        // Assert
        Assert.Equal("Assert.True(2 + 2 == 4)", result);
    }

    [Fact]
    public void TestCollectNamespaces()
    {
        // Arrange
        XUnitFrameworkModel sut = new();


        // Act
        var result = sut.CollectNamespaces();

        // Assert
        Assert.Equal(["Xunit"], result);
    }
}
