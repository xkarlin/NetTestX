using System.ComponentModel;
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
}
