using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generics;
using NSubstitute;
using Xunit;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Tests;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Generics.Tests;

public class GenericTypeResolverTests
{
    [Fact]
    public void TestResolveMethod()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
using System.Collections.Generic;

public class C
{
    public void M<T1, T2>()
        where T1 : IEnumerable<T2> { }
}
""");

        var testMethod = (IMethodSymbol)testCompilation.GetTypeByMetadataName("C").GetMembers().First(x => x.Name == "M");

        // Act
        var result = GenericTypeResolver.Resolve(testMethod, testCompilation);

        // Assert
        Assert.Equal("M<string, char>", result.ToDisplayString(CommonFormats.FullNullableFormat));
    }

    [Fact]
    public void TestResolveType()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
using System.Collections.Generic;

public class C<T1, T2>
    where T1 : IEnumerable<T2>
{ }
""");

        var testType = testCompilation.GetTypeByMetadataName("C`2");

        // Act
        var result = GenericTypeResolver.Resolve(testType, testCompilation);

        // Assert
        Assert.Equal("C<string, char>", result.ToDisplayString(CommonFormats.FullNullableFormat));
    }

    [Fact]
    public void TestResolveNamedType()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
using System.Collections.Generic;

public class C<T1, T2>
    where T1 : IEnumerable<T2>
{ }
""");

        var testType = testCompilation.GetTypeByMetadataName("C`2");

        // Act
        var result = GenericTypeResolver.Resolve(testType, testCompilation);

        // Assert
        Assert.Equal("C<string, char>", result.ToDisplayString(CommonFormats.FullNullableFormat));
    }
}
