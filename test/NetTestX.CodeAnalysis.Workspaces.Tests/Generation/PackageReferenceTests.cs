using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Generation;
using NSubstitute;
using Xunit;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Tests;

public class PackageReferenceTests
{
    [Fact]
    public void TestEquals()
    {
        // Arrange
        var name = "Test";
        var developmentOnly = true;
        PackageReference sut = new(name, developmentOnly);

        var testOther = new PackageReference("Test", true);

        // Act
        var result = sut.Equals(testOther);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestNotEquals()
    {
        // Arrange
        var name = "Test";
        var developmentOnly = true;
        PackageReference sut = new(name, developmentOnly);

        var testOther = new PackageReference("Test2", true);

        // Act
        var result = sut.Equals(testOther);

        // Assert
        Assert.False(result);
    }
}
