using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.Common;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis;

public partial class UnitTestGeneratorDriver
{
    public static Builder CreateBuilder(INamedTypeSymbol type, Compilation compilation) => new(type, compilation);

    public class Builder
    {
        public INamedTypeSymbol Type { get; }

        public Compilation Compilation { get; }

        public string TestClassName { get; set; }

        public string TestClassNamespace { get; set; }

        public TestFramework TestFramework { get; set; }

        public MockingLibrary MockingLibrary { get; set; }

        public IReadOnlyList<TestMethodModelBase> AllTestMethods { get; }

        public Dictionary<TestMethodModelBase, bool> TestMethodMap { get; }

        internal Builder(INamedTypeSymbol type, Compilation compilation)
        {
            Type = type;
            Compilation = compilation;
            AllTestMethods = MethodCollectorHelper.CollectTestMethods(Type, Compilation);
            TestMethodMap = AllTestMethods.ToDictionary(x => x, _ => true);
        }

        public UnitTestGeneratorDriver Build()
        {
            UnitTestGeneratorContext context = new()
            {
                Type = Type,
                Compilation = Compilation,
                Options = new()
                { 
                    TestClassName = TestClassName,
                    TestClassNamespace = TestClassNamespace,
                    MockingLibrary = MockingLibrary,
                    TestFramework = TestFramework,
                    TestMethods = TestMethodMap.Where(x => x.Value).Select(x => x.Key).ToArray()
                }
            };

            return new(context);
        }
    }
}
