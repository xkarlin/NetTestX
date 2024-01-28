using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.ViewModels;

namespace NetTestX.VSIX.UI.Views;

/// <summary>
/// Interaction logic for GenerateTestsAdvancedView.xaml
/// </summary>
public partial class GenerateTestsAdvancedView
{
    private readonly GenerateTestsAdvancedViewModel _viewModel;

    private readonly IReadOnlyList<string> _testProjects;

    public GenerateTestsAdvancedView(GenerateTestsAdvancedModel model, IEnumerable<string> testProjects)
    {
        DataContext = _viewModel = new(model);
        
        _testProjects = testProjects.ToList();
        
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
    
        TestProjectComboBox.ItemsSource = _testProjects;
        TestProjectComboBox.SelectedIndex = 0;

        _viewModel.TestProject = _testProjects[0];
    }

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
