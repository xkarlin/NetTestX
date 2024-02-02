namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public interface ITestFrameworkModel : INamespaceCollector
{
    string TestClassAttribute { get; }

    string TestMethodAttribute { get; }

    string AssertTrue(string expression);
}
