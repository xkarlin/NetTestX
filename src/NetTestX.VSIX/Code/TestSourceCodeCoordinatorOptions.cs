namespace NetTestX.VSIX.Code;

/// <summary>
/// Options passed to <see cref="TestSourceCodeCoordinator"/>
/// </summary>
public class TestSourceCodeCoordinatorOptions
{
    /// <summary>
    /// The name of the test file
    /// </summary>
    public required string TestFileName { get; set; }
}
