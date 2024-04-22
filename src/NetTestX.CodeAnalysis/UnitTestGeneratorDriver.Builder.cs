﻿using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.Common;
using System.Collections.Generic;
using System.Linq;
using NetTestX.Common.Diagnostics;
using NetTestX.CodeAnalysis.Generics;
using NetTestX.CodeAnalysis.Generation;

namespace NetTestX.CodeAnalysis;

public partial class UnitTestGeneratorDriver
{
    public static Builder CreateBuilder(
        INamedTypeSymbol type,
        Compilation compilation,
        AdvancedGeneratorOptions advancedOptions = AdvancedGeneratorOptions.None,
        IDiagnosticReporter reporter = null)
        => new(type, compilation, advancedOptions, reporter);

    public class Builder
    {
        private readonly IDiagnosticReporter _reporter;

        public INamedTypeSymbol Type { get; }

        public Compilation Compilation { get; }

        public string TestClassName { get; set; }

        public string TestClassNamespace { get; set; }

        public TestFramework TestFramework { get; set; }

        public MockingLibrary MockingLibrary { get; set; }

        public AdvancedGeneratorOptions AdvancedOptions { get; set; }

        public IReadOnlyList<TestMethodModelBase> AllTestMethods { get; }

        public Dictionary<TestMethodModelBase, bool> TestMethodMap { get; }

        internal Builder(INamedTypeSymbol type, Compilation compilation, AdvancedGeneratorOptions advancedOptions, IDiagnosticReporter reporter)
        {
            _reporter = reporter;
            Type = SymbolGenerationResolver.Resolve(type, compilation, advancedOptions);
            Compilation = compilation;
            AllTestMethods = MethodCollectorHelper.CollectTestMethods(Type, Compilation, advancedOptions, _reporter);
            TestMethodMap = AllTestMethods.ToDictionary(x => x, _ => true);
        }

        public UnitTestGeneratorDriver Build()
        {
            UnitTestGeneratorContext context = new()
            {
                Type = Type,
                Compilation = Compilation,
                TestMethods = TestMethodMap.Where(x => x.Value).Select(x => x.Key).ToArray(),
                Options = new()
                { 
                    TestClassName = TestClassName,
                    TestClassNamespace = TestClassNamespace,
                    MockingLibrary = MockingLibrary,
                    TestFramework = TestFramework,
                    AdvancedOptions = AdvancedOptions
                }
            };

            return new(context);
        }
    }
}