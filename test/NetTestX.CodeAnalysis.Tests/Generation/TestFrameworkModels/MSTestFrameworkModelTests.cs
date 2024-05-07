using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NSubstitute;
using Xunit;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels.Tests;

public class MSTestFrameworkModelTests
{
    [Fact]
    public void TestAssertTrue()
    {
        // Arrange
        MSTestFrameworkModel sut = new();

        var testExpression = "2 + 2 == 4";

        // Act
        var result = sut.AssertTrue(testExpression);

        // Assert
        Assert.Equal("Assert.IsTrue(2 + 2 == 4)", result);
    }

    [Fact]
    public void TestCollectNamespaces()
    {
        // Arrange
        MSTestFrameworkModel sut = new();


        // Act
        var result = sut.CollectNamespaces();

        // Assert
        Assert.Equal(["Microsoft.VisualStudio.TestTools.UnitTesting"], result);
    }
}
