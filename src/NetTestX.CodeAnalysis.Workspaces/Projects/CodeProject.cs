using System.IO;
using Microsoft.Build.Construction;
using System.Xml;
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

        using var xmlReader = XmlReader.Create(File.OpenRead(filePath));
        var root = ProjectRootElement.Create(xmlReader, new(), true);
        _project = new(root);
    }

    public string GetPropertyValue(string name) => _project.GetPropertyValue(name);

    public IEnumerable<CodeProjectItem> GetItems(string name) => _project.GetItems(name).Select(x => new CodeProjectItem(name, x.EvaluatedInclude));
}
