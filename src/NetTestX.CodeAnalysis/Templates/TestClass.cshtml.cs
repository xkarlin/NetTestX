using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Templates;

public class TestClassModel
{
    public string TestClassName { get; set; }

    public string TestClassNamespace { get; set; }

    public IEnumerable<TestMethodModel> TestMethods { get; set; }
}
