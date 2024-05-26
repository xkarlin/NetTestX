using System;

namespace NetTestX.VSIX.Diagnostics;

/// <summary>
/// Represents a logger that consumes messages
/// </summary>
public interface IMessageLogger : IDisposable
{
    /// <summary>
    /// Log the provided <paramref name="message"/>
    /// </summary>
    void LogMessage(string message);
}
