using System.Collections.Generic;
using NetTestX.Common.Diagnostics;

namespace NetTestX.VSIX.Code;

public class TestSourceCodeDiagnosticReporter : IDiagnosticReporter
{
    private readonly List<(DiagnosticSeverity, string)> _diagnostics = [];

    public IReadOnlyList<(DiagnosticSeverity Severity, string Message)> Diagnostics => _diagnostics;

    public void ReportDiagnostic(DiagnosticSeverity severity, string message) => _diagnostics.Add((severity, message));
}
