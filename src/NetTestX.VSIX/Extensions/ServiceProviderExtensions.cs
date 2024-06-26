﻿using System;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceProvider"/>
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Get an active text view (document) inside the editor window
    /// </summary>
    public static IWpfTextView GetActiveTextView(this IServiceProvider provider)
    {
        var textManager = (IVsTextManager)provider.GetService(typeof(SVsTextManager));

        if (textManager is null)
            return null;

        textManager.GetActiveView(1, null, out var vsTextView);

        var componentModel = (IComponentModel)provider.GetService(typeof(SComponentModel));
        var adapterService = componentModel?.GetService<Microsoft.VisualStudio.Editor.IVsEditorAdaptersFactoryService>();

        return adapterService?.GetWpfTextView(vsTextView);
    }
}
