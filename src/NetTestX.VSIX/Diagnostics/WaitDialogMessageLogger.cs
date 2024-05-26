using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace NetTestX.VSIX.Diagnostics;

/// <summary>
/// <see cref="IMessageLogger"/> that logs messages inside the VS wait dialog
/// </summary>
public class WaitDialogMessageLogger : IMessageLogger
{
    public string Title { get; }
    
    private readonly IVsThreadedWaitDialog2 _waitDialog;
    
    public WaitDialogMessageLogger(string title)
    {
        Title = title;
        
        ThreadHelper.ThrowIfNotOnUIThread();
        var dialogFactory = VS.GetRequiredService<SVsThreadedWaitDialogFactory, IVsThreadedWaitDialogFactory>();
        dialogFactory.CreateInstance(out _waitDialog);
    }

    public void LogMessage(string message)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        _waitDialog.StartWaitDialog(Title, message, string.Empty, null, message, 0, false, true);
    }

    public void Dispose()
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        _waitDialog.EndWaitDialog();
    }
}
