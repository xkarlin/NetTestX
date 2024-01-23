using System.ComponentModel;
using NetTestX.VSIX.Models;

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
}
