namespace NetTestX.Common.Diagnostics;

public interface IDiagnosticReporter
{
    void ReportDiagnostic(DiagnosticSeverity severity, string message);
}
