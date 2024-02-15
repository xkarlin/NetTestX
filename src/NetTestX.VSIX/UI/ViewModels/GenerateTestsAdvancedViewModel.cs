using System.Collections.Generic;
using System.ComponentModel;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.VSIX.UI.Models;

namespace NetTestX.VSIX.UI.ViewModels;

public class GenerateTestsAdvancedViewModel(GenerateTestsAdvancedModel model) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public string TestProject
    {
        get => model.TestProject;
        set
        {
            model.TestProject = value;
            PropertyChanged?.Invoke(this, new(nameof(TestProject)));
        }
    }

    public string TestFileName
    {
        get => model.TestFileName;
        set
        {
            model.TestFileName = value;
            PropertyChanged?.Invoke(this, new(nameof(TestFileName)));
        }
    }

    public string TestClassName
    {
        get => model.TestClassName;
        set
        {
            model.TestClassName = value;
            PropertyChanged?.Invoke(this, new(nameof(TestClassName)));
        }
    }

    public string TestClassNamespace
    {
        get => model.TestClassNamespace;
        set
        {
            model.TestClassNamespace = value;
            PropertyChanged?.Invoke(this, new(nameof(TestClassNamespace)));
        }
    }

    public Dictionary<TestMethodModelBase, bool> TestMethodMap
    {
        get => model.TestMethodMap;
        set
        {
            model.TestMethodMap = value;
            PropertyChanged?.Invoke(this, new(nameof(TestMethodMap)));
        }
    }

    public AdvancedGeneratorOptions AdvancedOptions
    {
        get => model.AdvancedOptions;
        set
        {
            model.AdvancedOptions = value;
            PropertyChanged?.Invoke(this, new(nameof(AdvancedOptions)));
        }
    }
}
