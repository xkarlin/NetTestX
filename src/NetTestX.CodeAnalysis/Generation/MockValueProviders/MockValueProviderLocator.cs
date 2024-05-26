using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

/// <summary>
/// Helper class used to find an instance of <see cref="IMockValueProvider"/> for the given <see cref="MockingLibrary"/>
/// </summary>
public static class MockValueProviderLocator
{
    /// <summary>
    /// Find an instance of <see cref="IMockValueProvider"/> for the given <paramref name="library"/>
    /// </summary>
    public static IMockValueProvider LocateValueProvider(MockingLibrary library) => library switch
    {
        MockingLibrary.NSubstitute => new NSubstituteValueProvider(),
        MockingLibrary.FakeItEasy => new FakeItEasyValueProvider(),
        MockingLibrary.Moq => new MoqValueProvider(),
        _ => throw new NotSupportedException()
    };
}
