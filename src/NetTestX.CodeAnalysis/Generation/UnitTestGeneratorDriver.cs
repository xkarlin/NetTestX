namespace NetTestX.CodeAnalysis.Generation;

public class UnitTestGeneratorDriver(UnitTestGeneratorContext context)
{
    public string GenerateTestClassSource()
    {
        return
$$"""
namespace {{context.Options.TestClassNamespace}};
               
public class {{context.Options.TestClassName}}
{
    
}
""";
    }
}
