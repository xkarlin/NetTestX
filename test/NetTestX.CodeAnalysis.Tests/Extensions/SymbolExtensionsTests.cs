using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Extensions;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Tests;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Extensions.Tests;

public class SymbolExtensionsTests
{
    [Fact]
    public void TestIsNumericType()
    {
        // Arrange
        var testType = Substitute.For<ITypeSymbol>();
        testType.SpecialType.Returns(SpecialType.System_Byte);

        // Act
        var result = SymbolExtensions.IsNumericType(testType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestImplementsInterfaceGeneric()
    {
        // Arrange
        var testIface = Substitute.For<INamedTypeSymbol>();
        testIface.Name.Returns("IDisposable");

        var testType = Substitute.For<ITypeSymbol>();
        testType.AllInterfaces.Returns([testIface]);

        // Act
        var result = SymbolExtensions.ImplementsInterface<IDisposable>(testType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestFindImplementationsForInterfaceMembers()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
using System;

public class C : IDisposable
{
    void IDisposable.Dispose() { }
}
""");

        var testTypeSymbol = testCompilation.GetTypeByMetadataName("C");

        // Act
        var result = SymbolExtensions.FindImplementationsForInterfaceMembers<IDisposable>(testTypeSymbol, testCompilation);

        // Assert
        Assert.Single(result);
        Assert.Equal("System.IDisposable.Dispose", result.First().Name);
    }

    [Fact]
    public void TestIsGenericTypeDefinition()
    {
        // Arrange
        var testType = Substitute.For<INamedTypeSymbol>();
        testType.Kind.Returns(SymbolKind.NamedType);
        testType.OriginalDefinition.Returns(testType);
        testType.IsGenericType.Returns(true);
        testType.Name.Returns("Foo");
        testType.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");

        // Act
        var result = SymbolExtensions.IsGenericTypeDefinition(testType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsGenericMethodDefinition()
    {
        // Arrange
        var testMethod = Substitute.For<IMethodSymbol>();
        testMethod.IsGenericMethod.Returns(true);
        testMethod.OriginalDefinition.Returns(testMethod);
        testMethod.Name.Returns("Foo");
        testMethod.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");

        // Act
        var result = SymbolExtensions.IsGenericMethodDefinition(testMethod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestImplementsInterface()
    {
        // Arrange
        var testIface = Substitute.For<INamedTypeSymbol>();
        testIface.Name.Returns("ITest");
        testIface.ToDisplayString(CommonFormats.FullNullableFormat).Returns("ITest");

        var testType = Substitute.For<ITypeSymbol>();
        testType.AllInterfaces.Returns([testIface]);

        // Act
        var result = SymbolExtensions.ImplementsInterface(testType, testIface);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestImplementsGenericInterface()
    {
        // Arrange
        var testIface = Substitute.For<INamedTypeSymbol>();
        testIface.Name.Returns("ITest");
        testIface.OriginalDefinition.Returns(testIface);
        testIface.ToDisplayString(CommonFormats.FullNullableFormat).Returns("ITest");

        var testType = Substitute.For<ITypeSymbol>();
        testType.AllInterfaces.Returns([testIface]);

        // Act
        var result = SymbolExtensions.ImplementsGenericInterface(testType, testIface);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestFindAllGenericInterfaceImplementations()
    {
        // Arrange
        var testIface = Substitute.For<INamedTypeSymbol>();
        testIface.OriginalDefinition.Returns(testIface);
        testIface.Name.Returns("Foo");
        testIface.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");
        
        var testType = Substitute.For<ITypeSymbol>();
        testType.AllInterfaces.Returns([testIface, testIface]);

        // Act
        var result = SymbolExtensions.FindAllGenericInterfaceImplementations(testType, testIface);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void TestIsInheritedFrom()
    {
        // Arrange
        var testBaseType = Substitute.For<INamedTypeSymbol>();
        testBaseType.BaseType.Returns(default(INamedTypeSymbol));
        testBaseType.IsReferenceType.Returns(true);
        testBaseType.Name.Returns("BaseFoo");
        testBaseType.ToDisplayString(CommonFormats.FullNullableFormat).Returns("BaseFoo");

        var testType = Substitute.For<ITypeSymbol>();
        testType.BaseType.Returns(testBaseType);
        testType.IsReferenceType.Returns(true);
        testType.Name.Returns("Foo");
        testType.ToDisplayString(CommonFormats.FullNullableFormat).Returns("Foo");

        // Act
        var result = SymbolExtensions.IsInheritedFrom(testType, testBaseType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsInheritedFromGenericType()
    {
        // Arrange
        var testBaseType = Substitute.For<INamedTypeSymbol>();
        testBaseType.OriginalDefinition.Returns(testBaseType);

        var testType = Substitute.For<ITypeSymbol>();
        testType.BaseType.Returns(testBaseType);

        // Act
        var result = SymbolExtensions.IsInheritedFromGenericType(testType, testBaseType);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestHasAccessibleConstructor()
    {
        // Arrange
        var testAccessibility = Microsoft.CodeAnalysis.Accessibility.Internal;

        var testConstructor = Substitute.For<IMethodSymbol>();
        testConstructor.DeclaredAccessibility.Returns(testAccessibility);

        var testType = Substitute.For<INamedTypeSymbol>();
        testType.Constructors.Returns([testConstructor]);

        // Act
        var result = SymbolExtensions.HasAccessibleConstructor(testType, testAccessibility);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestTryGetAccessibleConstructor()
    {
        // Arrange
        var testAccessibility = Microsoft.CodeAnalysis.Accessibility.Internal;
        
        var testConstructor = Substitute.For<IMethodSymbol>();
        testConstructor.DeclaredAccessibility.Returns(testAccessibility);

        var testType = Substitute.For<INamedTypeSymbol>();
        testType.Constructors.Returns([testConstructor]);

        // Act
        var result = SymbolExtensions.TryGetAccessibleConstructor(testType, testAccessibility, out var ctor);

        // Assert
        Assert.True(result);
        Assert.Equal(testConstructor, ctor);
    }

    [Fact]
    public void TestGetEffectiveAccessibility()
    {
        // Arrange
        var testContainingType = Substitute.For<INamedTypeSymbol>();
        testContainingType.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Internal);
        testContainingType.ContainingType.Returns(default(INamedTypeSymbol));

        var testSymbol = Substitute.For<ISymbol>();
        testSymbol.DeclaredAccessibility.Returns(Microsoft.CodeAnalysis.Accessibility.Public);
        testSymbol.ContainingType.Returns(testContainingType);

        // Act
        var result = SymbolExtensions.GetEffectiveAccessibility(testSymbol);

        // Assert
        Assert.Equal(Microsoft.CodeAnalysis.Accessibility.Internal, result);
    }

    [Fact]
    public void TestCollectNamespaces()
    {
        // Arrange
        var testNs = Substitute.For<INamespaceSymbol>();
        testNs.ToDisplayString().Returns("TestNamespace");

        var testSymbol = Substitute.For<ISymbol>();
        testSymbol.ContainingNamespace.Returns(testNs);
        testSymbol.ContainingType.Returns(default(INamedTypeSymbol));

        // Act
        var result = SymbolExtensions.CollectNamespaces(testSymbol);

        // Assert
        Assert.Equal(["TestNamespace"], result);
    }
}
