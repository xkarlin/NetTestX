﻿using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Generation.MockValueProviders;

public static class MockValueProviderLocator
{
    public static IMockValueProvider LocateValueProvider(MockingLibrary library) => library switch
    {
        MockingLibrary.NSubstitute => new NSubstituteValueProvider(),
        MockingLibrary.FakeItEasy => new FakeItEasyValueProvider(),
        _ => throw new NotSupportedException()
    };
}
