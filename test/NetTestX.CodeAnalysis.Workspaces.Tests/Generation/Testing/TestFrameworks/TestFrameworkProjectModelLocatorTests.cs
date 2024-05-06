using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;
using NSubstitute;
using Xunit;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks.Tests;

public class TestFrameworkProjectModelLocatorTests
{
    [Fact]
    public void TestLocateNUnitModel()
    {
        // Arrange
        var testFramework = TestFramework.NUnit;

        // Act
        var result = TestFrameworkProjectModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<NUnitProjectModel>(result);
        Assert.Equal(TestFramework.NUnit, result.Framework);
        Assert.Equal(
        [
                new("NUnit"),
                new("NUnit3TestAdapter"),
                new("NUnit.Analyzers")
            ], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }

    [Fact]
    public void TestLocateXUnitModel()
    {
        // Arrange
        var testFramework = TestFramework.XUnit;

        // Act
        var result = TestFrameworkProjectModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<XUnitProjectModel>(result);
        Assert.Equal(TestFramework.XUnit, result.Framework);
        Assert.Equal(
        [
                new("xunit"),
                new("xunit.runner.visualstudio", true)
            ], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }

    [Fact]
    public void TestLocateMSTestModel()
    {
        // Arrange
        var testFramework = TestFramework.MSTest;

        // Act
        var result = TestFrameworkProjectModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<MSTestProjectModel>(result);
        Assert.Equal(TestFramework.MSTest, result.Framework);
        Assert.Equal(
        [
                new("MSTest.TestAdapter"),
                new("MSTest.TestFramework")
            ], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }
}
