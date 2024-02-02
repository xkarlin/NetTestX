﻿using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Code;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestsAdvancedEditorCommandHandler(DTE2 dte, INamedTypeSymbol typeSymbol) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var sourceProject = dte.ActiveDocument.ProjectItem.ContainingProject;

        await TestSourceCodeUtility.LoadSourceCodeFromAdvancedViewAsync(dte, sourceProject, typeSymbol);
    }
}
