namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

public interface ITestFrameworkModel : INamespaceCollector
{
    string TestMethodAttribute { get; }
}
