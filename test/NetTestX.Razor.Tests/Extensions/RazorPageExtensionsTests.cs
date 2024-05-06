using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.Razor.Extensions;
using NSubstitute;
using Xunit;
using NetTestX.Razor;

namespace NetTestX.Razor.Extensions.Tests;

public class RazorPageExtensionsTests
{
    [Fact]
    public async Task TestImportPagesAsync()
    {
        // Arrange
        var testPage = new TestRazorPage();
        var testModels = new object[] { new TestModel() };
        var testSeparator = "";

        // Act
        var result = await RazorPageExtensions.ImportPagesAsync(testPage, testModels, testSeparator);

        // Assert
        Assert.Equal("foo", result);
    }

    private class TestModel;

    private class TestRazorPage : RazorPage<TestModel>
    {
        public override Task ExecuteAsync()
        {
            WriteLiteral("foo");
            return Task.CompletedTask;
        }
    }
}
