using NetTestX.CodeAnalysis.Generation;

namespace NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

public interface IMethodBodyModel : INamespaceCollector
{
    TestMethodModelBase Parent { get; set; }

    string GetDisplayName();
}
