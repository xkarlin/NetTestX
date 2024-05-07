using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NSubstitute;
using Xunit;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels.Tests;

public class TestFrameworkModelLocatorTests
{
    [Fact]
    public void TestLocateXUnitModel()
    {
        // Arrange
        var testFramework = TestFramework.XUnit;

        // Act
        var result = TestFrameworkModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<XUnitFrameworkModel>(result);
    }

    [Fact]
    public void TestLocateNUnitModel()
    {
        // Arrange
        var testFramework = TestFramework.NUnit;

        // Act
        var result = TestFrameworkModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<NUnitFrameworkModel>(result);
    }

    [Fact]
    public void TestLocateMSTestModel()
    {
        // Arrange
        var testFramework = TestFramework.MSTest;

        // Act
        var result = TestFrameworkModelLocator.LocateModel(testFramework);

        // Assert
        Assert.IsType<MSTestFrameworkModel>(result);
    }
}
