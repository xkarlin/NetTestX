namespace NetTestX.CodeAnalysis.Generation.TestFrameworkModels;

/// <summary>
/// Represents a data model for testing frameworks
/// </summary>
public interface ITestFrameworkModel : INamespaceCollector
{
    /// <summary>
    /// Attribute used to annotate a generated test class
    /// </summary>
    string TestClassAttribute { get; }

    /// <summary>
    /// Attribute used to annotate a generated test method
    /// </summary>
    string TestMethodAttribute { get; }

    /// <summary>
    /// Generate Assert.True or similar method for the given <paramref name="expression"/>
    /// </summary>
    string AssertTrue(string expression);
}
