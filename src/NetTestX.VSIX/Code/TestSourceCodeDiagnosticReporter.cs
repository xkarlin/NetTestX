using System.Collections.Generic;
using NetTestX.Common.Diagnostics;

namespace NetTestX.VSIX.Code;

/// <summary>
/// Implementation of <see cref="IDiagnosticReporter"/> used during test source code generation
/// </summary>
public class TestSourceCodeDiagnosticReporter : IDiagnosticReporter
{
    private readonly List<(DiagnosticSeverity, string)> _diagnostics = [];

    public IReadOnlyList<(DiagnosticSeverity Severity, string Message)> Diagnostics => _diagnostics;

    public void ReportDiagnostic(DiagnosticSeverity severity, string message) => _diagnostics.Add((severity, message));
}
