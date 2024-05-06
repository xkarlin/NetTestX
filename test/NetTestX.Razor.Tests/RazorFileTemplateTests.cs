using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.Razor;
using NSubstitute;
using Xunit;

namespace NetTestX.Razor.Tests;

public class RazorFileTemplateTests
{
    [Fact]
    public async Task TestRenderAsync()
    {
        // Arrange
        var model = new TestModel("foo");
        RazorFileTemplate sut = new(model);


        // Act
        var result = await sut.RenderAsync();

        // Assert
        Assert.Equal($"Text: foo", result);
    }

    private class TestModel(string text)
    {
        public string Text = text;
    }

    private class TestPage : RazorPage<TestModel>
    {
        public override Task ExecuteAsync()
        {
            WriteLiteral($"Text: {Model.Text}");
            return Task.CompletedTask;
        }
    }
}
