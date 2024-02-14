using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.ViewModels;
using NetTestX.Polyfill.Extensions;

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
        InitializeTestProjectComboBox();
        InitializeTestMethodsPanel();
    }

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void TestMethodCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var testMethod = (TestMethodModelBase)checkBox.DataContext;

        _viewModel.TestMethodMap[testMethod] = true;
    }

    private void TestMethodCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var testMethod = (TestMethodModelBase)checkBox.DataContext;

        _viewModel.TestMethodMap[testMethod] = false;
    }

    private void InitializeTestProjectComboBox()
    {
        TestProjectComboBox.ItemsSource = _testProjects;
        TestProjectComboBox.SelectedIndex = 0;
    }

    private void InitializeTestMethodsPanel()
    {
        foreach (var (testMethod, enabled) in _viewModel.TestMethodMap)
        {
            CheckBox box = new()
            {
                Content = testMethod.MethodBodyModel.GetDisplayName(),
                DataContext = testMethod,
                IsChecked = enabled
            };

            box.Checked += TestMethodCheckBox_Checked;
            box.Unchecked += TestMethodCheckBox_Unchecked;

            TestMethodsPanel.Children.Add(box);
        }
    }
}
