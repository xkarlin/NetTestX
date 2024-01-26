﻿using System.IO;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects;

public class CodeProject
{
    private readonly Project _project;

    public string Name => Path.GetFileNameWithoutExtension(FilePath);

    public string FilePath { get; }

    public CodeProject(string filePath)
    {
        FilePath = filePath;

        var root = ProjectRootElement.Open(FilePath, new(), true);
        _project = new(root);
    }

    public string GetPropertyValue(string name) => _project.GetPropertyValue(name);

    public void AddItem(string name, string value) => _project.AddItem(name, value);
    
    public IEnumerable<CodeProjectItem> GetItems(string name) => _project.GetItems(name).Select(x => new CodeProjectItem(name, x.EvaluatedInclude));

    public void Save() => _project.Save(FilePath);
}
