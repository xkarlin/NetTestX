namespace NetTestX.CodeAnalysis.Generation;

public class UnitTestGeneratorDriver(UnitTestGeneratorContext context)
{
    public string GenerateTestClassSource()
    {
        return
$$"""
namespace {{context.Type.ContainingNamespace}}.Tests;
               
public class {{context.Type.Name}}Tests
{
    
}
""";
    }
}
