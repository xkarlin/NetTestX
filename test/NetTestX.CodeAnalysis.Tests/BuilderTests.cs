using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.Common.Diagnostics;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Tests;

public class BuilderTests
{
    [Fact]
    public void TestBuild()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;
using System.Threading.Tasks;

public class C : IDisposable
{
    public void M1() { }

    public async Task M2() { }

    public static async void M3() { }

    public void Dispose() { }
}
""");

        var type = compilation.GetTypeByMetadataName("C");
        var advancedOptions = AdvancedGeneratorOptions.None;
        var reporter = Substitute.For<IDiagnosticReporter>();
        UnitTestGeneratorDriver.Builder sut = new(type, compilation, advancedOptions, reporter);

        sut.TestMethodMap[sut.AllTestMethods.First(x => x.MethodBodyModel is DisposableTypeMethodBodyModel)] = false;

        // Act
        var result = sut.Build();

        // Assert
        Assert.Equal(3, result.Context.TestMethods.Count);
        Assert.Equal(["TestM1", "TestM2", "TestM3"], result.Context.TestMethods.Select(x => x.MethodName).Order());
    }
}
