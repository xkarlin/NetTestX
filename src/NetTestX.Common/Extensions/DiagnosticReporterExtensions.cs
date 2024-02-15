using NetTestX.Common.Diagnostics;

namespace NetTestX.Common.Extensions;

public static class DiagnosticReporterExtensions
{
    public static void ReportInfo(this IDiagnosticReporter reporter, string message)
        => reporter.ReportDiagnostic(DiagnosticSeverity.Info, message);

    public static void ReportWarning(this IDiagnosticReporter reporter, string message)
        => reporter.ReportDiagnostic(DiagnosticSeverity.Warning, message);

    public static void ReportError(this IDiagnosticReporter reporter, string message)
        => reporter.ReportDiagnostic(DiagnosticSeverity.Error, message);
}
