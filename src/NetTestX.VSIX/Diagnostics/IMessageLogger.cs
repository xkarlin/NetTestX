using System;

namespace NetTestX.VSIX.Diagnostics;

public interface IMessageLogger : IDisposable
{
    void LogMessage(string message);
}
