namespace NetTestX.Common.Diagnostics;

/// <summary>
/// Represents a listener for diagnostics that may occur during workflows
/// </summary>
public interface IDiagnosticReporter
{
    /// <summary>
    /// Report a diagnostic to this listener
    /// </summary>
    void ReportDiagnostic(DiagnosticSeverity severity, string message);
}
