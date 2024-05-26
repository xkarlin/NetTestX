using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace NetTestX.CodeAnalysis.Workspaces.Projects;

/// <summary>
/// Represents a .NET project (e.g. *.csproj file for C# projects)
/// </summary>
public class CodeProject
{
    private readonly Project _project;

    /// <summary>
    /// The name of the project
    /// </summary>
    public string Name => Path.GetFileNameWithoutExtension(FilePath);

    /// <summary>
    /// The file path where the project is located
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// Create a code project for the file at the given <paramref name="filePath"/>
    /// </summary>
    public CodeProject(string filePath)
    {
        FilePath = filePath;
        
        _project = ProjectCollection.GlobalProjectCollection.LoadProject(FilePath);
    }

    /// <summary>
    /// Get the value of the given MSBuild property in the project
    /// </summary>
    public string GetPropertyValue(string name) => _project.GetPropertyValue(name);

    /// <summary>
    /// Add an MSBuild item with the given <paramref name="value"/> to the project
    /// </summary>
    public void AddItem(string name, string value) => _project.AddItem(name, value);
    
    /// <summary>
    /// Get all MSBuild items with the given <paramref name="name"/> from the project
    /// </summary>
    public IEnumerable<CodeProjectItem> GetItems(string name) => _project.GetItems(name).Select(x => new CodeProjectItem(name, x.EvaluatedInclude));

    /// <summary>
    /// Save this project to the file system
    /// </summary>
    public void Save() => _project.Save(FilePath);
}
