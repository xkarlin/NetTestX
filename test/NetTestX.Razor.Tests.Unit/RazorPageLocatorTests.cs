using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.Razor;
using NSubstitute;
using Xunit;

namespace NetTestX.Razor.Tests;

public class RazorPageLocatorTests
{
    [Fact]
    public void TestFindPage()
    {
        // Arrange
        var testType = typeof(TestModel);

        // Act
        var result = RazorPageLocator.FindPage(testType);

        // Assert
        Assert.IsType<TestPage>(result);
    }

    private class TestModel;

    private class TestPage : RazorPage<TestModel>
    {
        public override Task ExecuteAsync() => throw new UnreachableException();
    }
}
