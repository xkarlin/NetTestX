using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NSubstitute;
using Xunit;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders.Tests;

public class MockValueProviderLocatorTests
{
    [Fact]
    public void TestLocateMoqValueProvider()
    {
        // Arrange
        var testLibrary = MockingLibrary.Moq;

        // Act
        var result = MockValueProviderLocator.LocateValueProvider(testLibrary);

        // Assert
        Assert.IsType<MoqValueProvider>(result);
    }

    [Fact]
    public void TestLocateNSubstituteValueProvider()
    {
        // Arrange
        var testLibrary = MockingLibrary.NSubstitute;

        // Act
        var result = MockValueProviderLocator.LocateValueProvider(testLibrary);

        // Assert
        Assert.IsType<NSubstituteValueProvider>(result);
    }

    [Fact]
    public void TestLocateFakeItEasyValueProvider()
    {
        // Arrange
        var testLibrary = MockingLibrary.FakeItEasy;

        // Act
        var result = MockValueProviderLocator.LocateValueProvider(testLibrary);

        // Assert
        Assert.IsType<FakeItEasyValueProvider>(result);
    }
}
