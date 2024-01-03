using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Construction;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.CodeAnalysis.Workspaces;

public class CodeWorkspace
{
    private readonly string _solutionFilePath;

    private SolutionFile _solution;

    public IEnumerable<CodeProject> Projects => _solution.ProjectsInOrder.Select(x => new CodeProject(x.AbsolutePath));

    private CodeWorkspace(SolutionFile solution, string solutionFilePath)
    {
        _solution = solution;
        _solutionFilePath = solutionFilePath;
    }

    public async Task<CodeProject> CreateProjectAsync(string projectFilePath, string projectFile, Func<Task> saveCallback)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(projectFilePath)!);

        using (var writer = File.CreateText(projectFilePath))
            await writer.WriteAsync(projectFile);

        await saveCallback.Invoke();

        _solution = SolutionFile.Parse(_solutionFilePath);

        return new(projectFilePath);
    }

    public static CodeWorkspace Open(string solutionFilePath)
    {
        var solution = SolutionFile.Parse(solutionFilePath);
        return new(solution, solutionFilePath);
    }
}
