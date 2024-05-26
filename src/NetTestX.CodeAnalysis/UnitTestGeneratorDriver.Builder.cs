using Microsoft.CodeAnalysis;
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
    /// <summary>
    /// Create a <see cref="Builder"/> instance for <see cref="UnitTestGeneratorDriver"/>
    /// </summary>
    public static Builder CreateBuilder(
        INamedTypeSymbol type,
        Compilation compilation,
        AdvancedGeneratorOptions advancedOptions = AdvancedGeneratorOptions.None,
        IDiagnosticReporter reporter = null)
        => new(type, compilation, advancedOptions, reporter);

    /// <summary>
    /// Builder for <see cref="UnitTestGeneratorDriver"/>
    /// </summary>
    public class Builder
    {
        private readonly IDiagnosticReporter _reporter;

        /// <inheritdoc cref="UnitTestGeneratorContext.Type" />
        public INamedTypeSymbol Type { get; }

        /// <inheritdoc cref="UnitTestGeneratorContext.Compilation" />
        public Compilation Compilation { get; }

        /// <inheritdoc cref="UnitTestGeneratorOptions.TestClassName" />
        public string TestClassName { get; set; }

        /// <inheritdoc cref="UnitTestGeneratorOptions.TestClassNamespace" />
        public string TestClassNamespace { get; set; }

        /// <inheritdoc cref="UnitTestGeneratorOptions.TestFramework" />
        public TestFramework TestFramework { get; set; }

        /// <inheritdoc cref="UnitTestGeneratorOptions.MockingLibrary" />
        public MockingLibrary MockingLibrary { get; set; }

        /// <inheritdoc cref="UnitTestGeneratorOptions.AdvancedOptions" />
        public AdvancedGeneratorOptions AdvancedOptions { get; set; }

        /// <inheritdoc cref="UnitTestGeneratorContext.TestMethods" />
        public IReadOnlyList<TestMethodModelBase> AllTestMethods { get; }

        /// <summary>
        /// Map of all available <see cref="TestMethodModelBase"/> versus whether the should be generated (<c>true</c>) or not
        /// </summary>
        public Dictionary<TestMethodModelBase, bool> TestMethodMap { get; }

        internal Builder(INamedTypeSymbol type, Compilation compilation, AdvancedGeneratorOptions advancedOptions, IDiagnosticReporter reporter)
        {
            _reporter = reporter;
            Type = SymbolGenerationResolver.Resolve(type, compilation, advancedOptions);
            Compilation = compilation;
            AllTestMethods = MethodCollectorHelper.CollectTestMethods(Type, Compilation, advancedOptions, _reporter);
            TestMethodMap = AllTestMethods.ToDictionary(x => x, _ => true);
            AdvancedOptions = advancedOptions;
        }

        /// <summary>
        /// Build the <see cref="UnitTestGeneratorDriver"/>
        /// </summary>
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
