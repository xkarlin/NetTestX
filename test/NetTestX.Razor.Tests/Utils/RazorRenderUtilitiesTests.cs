using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.Razor.Utils;
using NSubstitute;
using Xunit;

namespace NetTestX.Razor.Utils.Tests;

public class RazorRenderUtilitiesTests
{
    [Fact]
    public void TestNormalizeIndentation()
    {
        // Arrange
        var testText =
"""
  class Test
{
    public int I;
}
""";
        var testIndentation = "  ";

        // Act
        var result = RazorRenderUtilities.NormalizeIndentation(testText, testIndentation);

        // Assert
        Assert.Equal(
"""
  class Test
  {
      public int I;
  }
""", result);
    }
}
