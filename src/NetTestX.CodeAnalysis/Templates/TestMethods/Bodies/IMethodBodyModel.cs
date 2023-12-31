using NetTestX.CodeAnalysis.Generation;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public interface IMethodBodyModel : INamespaceCollector
{
    ITestMethodModel Parent { get; set; }
}
