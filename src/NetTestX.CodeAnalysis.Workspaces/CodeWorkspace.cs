﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.CodeAnalysis.Workspaces;

/// <summary>
/// Represents a collection of <see cref="CodeProject"/>s
/// </summary>
public class CodeWorkspace
{
    private readonly string _solutionFilePath;

    private SolutionFile _solution;

    /// <summary>
    /// Projects that are contained in this workspace
    /// </summary>
    public IEnumerable<CodeProject> Projects => _solution.GetSolutionProjects().Select(x => new CodeProject(x.AbsolutePath));

    private CodeWorkspace(SolutionFile solution, string solutionFilePath)
    {
        _solution = solution;
        _solutionFilePath = solutionFilePath;
    }

    /// <summary>
    /// Create a new <see cref="CodeProject"/> inside this workspace
    /// </summary>
    public async Task<CodeProject> CreateProjectAsync(string projectFilePath, string projectFile, Func<Task> saveCallback)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(projectFilePath)!);

        using (var writer = File.CreateText(projectFilePath))
            await writer.WriteAsync(projectFile);

        await saveCallback.Invoke();

        _solution = SolutionFile.Parse(_solutionFilePath);

        return new(projectFilePath);
    }

    /// <summary>
    /// Open the workspace at the specified <paramref name="solutionFilePath"/>
    /// </summary>
    public static CodeWorkspace Open(string solutionFilePath)
    {
        var solution = SolutionFile.Parse(solutionFilePath);
        return new(solution, solutionFilePath);
    }
}
