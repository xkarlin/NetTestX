using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;
using NSubstitute;
using Xunit;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries.Tests;

public class MockingLibraryProjectModelLocatorTests
{
    [Fact]
    public void TestLocateNSubstituteModel()
    {
        // Arrange
        var testLibrary = MockingLibrary.NSubstitute;

        // Act
        var result = MockingLibraryProjectModelLocator.LocateModel(testLibrary);

        // Assert
        Assert.IsType<NSubstituteProjectModel>(result);
        Assert.Equal(MockingLibrary.NSubstitute, result.Library);
        Assert.Equal([new("NSubstitute")], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }

    [Fact]
    public void TestLocateMoqModel()
    {
        // Arrange
        var testLibrary = MockingLibrary.Moq;

        // Act
        var result = MockingLibraryProjectModelLocator.LocateModel(testLibrary);

        // Assert
        Assert.IsType<MoqProjectModel>(result);
        Assert.Equal(MockingLibrary.Moq, result.Library);
        Assert.Equal([new("Moq")], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }

    [Fact]
    public void TestLocateFakeItEasyModel()
    {
        // Arrange
        var testLibrary = MockingLibrary.FakeItEasy;

        // Act
        var result = MockingLibraryProjectModelLocator.LocateModel(testLibrary);

        // Assert
        Assert.IsType<FakeItEasyProjectModel>(result);
        Assert.Equal(MockingLibrary.FakeItEasy, result.Library);
        Assert.Equal([new("FakeItEasy")], result.PackageReferences, EqualityComparer<PackageReference>.Default);
    }
}
