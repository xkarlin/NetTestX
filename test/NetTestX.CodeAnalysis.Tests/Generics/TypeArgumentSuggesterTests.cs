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

namespace NetTestX.CodeAnalysis.Generics.Tests;

public class TypeArgumentSuggesterTests
{
    [Fact]
    public void TestEnumerateSuggestions()
    {
        // Arrange
        var testCompilation = CompilationUtility.CreateCompilation(
"""
public class C { }
""");

        // Act
        var result = TypeArgumentSuggester.EnumerateSuggestions(testCompilation);

        var enumerator = result.GetEnumerator();

        // Assert
        Assert.True(enumerator.MoveNext());
        Assert.Equal(SpecialType.System_Object, enumerator.Current.SpecialType);

        Assert.True(enumerator.MoveNext());
        Assert.Equal(SpecialType.System_Int32, enumerator.Current.SpecialType);

        Assert.True(enumerator.MoveNext());
        Assert.Equal(SpecialType.System_String, enumerator.Current.SpecialType);

        Assert.True(enumerator.MoveNext());
        Assert.Equal(SpecialType.System_Char, enumerator.Current.SpecialType);

        Assert.True(enumerator.MoveNext());
        Assert.Equal("List", enumerator.Current.Name);

        Assert.True(enumerator.MoveNext());
        Assert.Equal("HashSet", enumerator.Current.Name);

        Assert.True(enumerator.MoveNext());
        Assert.Equal("Dictionary", enumerator.Current.Name);

        Assert.True(enumerator.MoveNext());
        Assert.Equal("C", enumerator.Current.Name);
    }
}
