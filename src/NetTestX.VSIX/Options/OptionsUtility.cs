﻿using Microsoft.VisualStudio.OLE.Interop;
using NetTestX.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Options;

/// <summary>
/// Helper class for options
/// </summary>
public static class OptionsUtility
{
    /// <summary>
    /// Get <see cref="AdvancedGeneratorOptions"/> from the VS options
    /// </summary>
    public static async Task<AdvancedGeneratorOptions> GetAdvancedGeneratorOptionsAsync()
    {
        var codeOptions = await CodeOptions.GetLiveInstanceAsync();
        var generalOptions = await GeneralOptions.GetLiveInstanceAsync();

        var flags = AdvancedGeneratorOptions.None;

        if (codeOptions.IncludeAAAComments)
            flags |= AdvancedGeneratorOptions.IncludeAAAComments;

        if (codeOptions.UseSmartGenerics)
            flags |= AdvancedGeneratorOptions.UseSmartGenerics;

        if (generalOptions.TestInternalMembers)
            flags |= AdvancedGeneratorOptions.IncludeInternalMembers;

        return flags;
    }
}
