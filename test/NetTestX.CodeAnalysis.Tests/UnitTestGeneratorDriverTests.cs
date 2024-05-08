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
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Tests;

public class UnitTestGeneratorDriverTests
{
    [Fact]
    public async Task TestGenerateTestClassSourceAsync()
    {
        // Arrange
        var compilation = CompilationUtility.CreateCompilation(
"""
using System;
using System.Threading.Tasks;

public class C : IDisposable, IAsyncDisposable
{
    public string M1(int i) { }

    public async Task M2() { }

    public static string M3(int i) { }
    
    public static async void M4() { }

    public int this[string x]
    {
        get => 42;
        set => { }
    }

    public void Dispose() { }

    public async ValueTask DisposeAsync() { }
}
""");

        var type = compilation.GetTypeByMetadataName("C");

        var context = new UnitTestGeneratorContext
        {
            Compilation = compilation,
            Options = new()
            {
                MockingLibrary = NetTestX.Common.MockingLibrary.FakeItEasy,
                TestFramework = NetTestX.Common.TestFramework.XUnit,
                TestClassName = "TestsForC",
                TestClassNamespace = "TestNamespace"
            },
            Type = type,
            TestMethods =
            [
                new TestMethodModel(
                    type.GetMembers().First(x => x.Name == "M1"),
                    new AccessibleInstanceMethodBodyModel((IMethodSymbol)type.GetMembers().First(x => x.Name == "M1"), type.Constructors[0])),
                new AsyncTestMethodModel(
                    type.GetMembers().First(x => x.Name == "M2"),
                    new AccessibleInstanceMethodBodyModel((IMethodSymbol)type.GetMembers().First(x => x.Name == "M2"), type.Constructors[0])),
                new TestMethodModel(
                    type.GetMembers().First(x => x.Name == "M3"),
                    new AccessibleStaticMethodBodyModel((IMethodSymbol)type.GetMembers().First(x => x.Name == "M3"))),
                new TestMethodModel(
                    type.GetMembers().First(x => x.Name == "M4"),
                    new AccessibleStaticMethodBodyModel((IMethodSymbol)type.GetMembers().First(x => x.Name == "M4"))),
                new TestMethodModel(
                    type.GetMembers().First(x => x is IPropertySymbol { IsIndexer: true }),
                    new AccessibleIndexerMethodBodyModel((IPropertySymbol)type.GetMembers().First(x => x is IPropertySymbol { IsIndexer: true }), type.Constructors[0]),
                    "TestIndexer"),
                new TestMethodModel(
                    type,
                    new DisposableTypeMethodBodyModel(type.Constructors[0], false)),
                new AsyncTestMethodModel(
                    type,
                    new DisposableTypeMethodBodyModel(type.Constructors[0], true)),
            ]
        };

        UnitTestGeneratorDriver sut = new(context);


        // Act
        var result = await sut.GenerateTestClassSourceAsync();

        // Assert
        Assert.Equal(
"""
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using <global namespace>;
using FakeItEasy;
using Xunit;

namespace TestNamespace;

public class TestsForC
{
    [Fact]
    public void TestM1()
    {
        C sut = new();

        var testI = 0;

        var result = sut.M1(testI);


    }

    [Fact]
    public async Task TestM2()
    {
        C sut = new();


        await sut.M2();


    }

    [Fact]
    public void TestM3()
    {
        var testI = 0;

        var result = C.M3(testI);


    }

    [Fact]
    public void TestM4()
    {
    
        C.M4();


    }

    [Fact]
    public void TestIndexer()
    {
        C sut = new();

        var testX = "";
        var toSet = 0;

        sut[testX] = toSet;
        var result = sut[testX];


    }

    [Fact]
    public void TestC()
    {
        IDisposable sut = new C();

        using (sut) { }

        // Check that the object ignores all calls to Dispose after the first one (and does not throw)
        sut.Dispose();


    }

    [Fact]
    public async Task TestC()
    {
        IAsyncDisposable sut = new C();

        await using (sut) { }

        // Check that the object ignores all calls to Dispose after the first one (and does not throw)
        var disposeTask = sut.DisposeAsync();

        Assert.True(disposeTask.IsCompleted);

    }
}

""", result);
    }

    [Fact]
    public void TestCreateBuilder()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
using System.Collections.Generic;

public class C<T1, T2> : IDisposable
    where T1 : IEnumerable<T2>
{ }
""");

        var testType = testCompilation.GetTypeByMetadataName("C`2");
        var testAdvancedOptions = AdvancedGeneratorOptions.UseSmartGenerics;
        var testReporter = Substitute.For<IDiagnosticReporter>();

        // Act
        var result = UnitTestGeneratorDriver.CreateBuilder(testType, testCompilation, testAdvancedOptions, testReporter);

        // Assert
        Assert.Equal("C<string, char>", result.Type.ToDisplayString(CommonFormats.FullNullableFormat));
        Assert.Equal(testCompilation, result.Compilation);
        Assert.Equal(testAdvancedOptions, result.AdvancedOptions);
    }
}
