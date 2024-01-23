using System.Windows;
using System.Windows.Forms;
using NetTestX.VSIX.Models;
using NetTestX.VSIX.UI.ViewModels;

namespace NetTestX.VSIX.UI.Views;

public partial class GenerateTestProjectView
{
    private readonly GenerateTestProjectViewModel _viewModel;

    public GenerateTestProjectView(GenerateTestProjectModel model)
    {
        DataContext = _viewModel = new(model);
        InitializeComponent();
    }

    private void ProjectDirectoryButton_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
     
        if (result == System.Windows.Forms.DialogResult.OK)
            _viewModel.ProjectDirectory = dialog.SelectedPath;
    }

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
