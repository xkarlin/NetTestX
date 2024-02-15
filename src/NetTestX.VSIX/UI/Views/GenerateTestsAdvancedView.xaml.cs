using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.ViewModels;
using NetTestX.Polyfill.Extensions;
using NetTestX.VSIX.Code;

namespace NetTestX.VSIX.UI.Views;

/// <summary>
/// Interaction logic for GenerateTestsAdvancedView.xaml
/// </summary>
public partial class GenerateTestsAdvancedView
{
    private const int MAX_DIAGNOSTICS_TO_SHOW = 10;

    private readonly TestSourceCodeDiagnosticReporter _reporter;

    private readonly GenerateTestsAdvancedViewModel _viewModel;

    private readonly IReadOnlyList<string> _testProjects;

    public GenerateTestsAdvancedView(
        GenerateTestsAdvancedModel model,
        TestSourceCodeDiagnosticReporter reporter,
        IEnumerable<string> testProjects)
    {
        _reporter = reporter;
        DataContext = _viewModel = new(model);
        
        _testProjects = testProjects.ToList();
        
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        InitializeTestProjectComboBox();
        InitializeDiagnosticsPanel();
        InitializeTestMethodsPanel();
        InitializeAdvancedOptionsCheckBoxes();
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

    private void IncludeAAACommentsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        _viewModel.AdvancedOptions |= AdvancedGeneratorOptions.IncludeAAAComments;
    }

    private void IncludeAAACommentsCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        _viewModel.AdvancedOptions &= ~AdvancedGeneratorOptions.IncludeAAAComments;
    }

    private void InitializeTestProjectComboBox()
    {
        TestProjectComboBox.ItemsSource = _testProjects;
        TestProjectComboBox.SelectedIndex = 0;
    }

    private void InitializeDiagnosticsPanel()
    {
        foreach (var (severity, message) in _reporter.Diagnostics.OrderByDescending(x => x.Severity).Take(MAX_DIAGNOSTICS_TO_SHOW))
        {
            TextBlock block = new()
            {
                Text = message,
                FontSize = 16
            };

            CodeDiagnosticsPanel.Children.Add(block);
        }
    }

    private void InitializeTestMethodsPanel()
    {
        if (_viewModel.TestMethodMap.Count == 0)
        {
            TextBlock noMethodsBlock = new()
            {
                Text = "No test methods available for generation.",
                FontSize = 16
            };

            TestMethodsPanel.Children.Add(noMethodsBlock);
            return;
        }

        foreach (var (testMethod, enabled) in _viewModel.TestMethodMap)
        {
            CheckBox box = new()
            {
                Content = testMethod.MethodBodyModel.GetDisplayName(),
                DataContext = testMethod,
                IsChecked = enabled,
                FontSize = 16
            };

            box.Checked += TestMethodCheckBox_Checked;
            box.Unchecked += TestMethodCheckBox_Unchecked;

            TestMethodsPanel.Children.Add(box);
        }
    }

    private void InitializeAdvancedOptionsCheckBoxes()
    {
        IncludeAAACommentsCheckBox.IsChecked = (_viewModel.AdvancedOptions & AdvancedGeneratorOptions.IncludeAAAComments) != 0;
    }
}
