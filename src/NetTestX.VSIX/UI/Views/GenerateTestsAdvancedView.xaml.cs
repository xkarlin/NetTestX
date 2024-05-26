using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.ViewModels;
using NetTestX.Polyfill.Extensions;
using NetTestX.VSIX.Code;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.VSIX.Options.Validation;
using System.Windows.Media;

namespace NetTestX.VSIX.UI.Views;

public partial class GenerateTestsAdvancedView
{
    private const int MAX_DIAGNOSTICS_TO_SHOW = 10;

    private static readonly IReadOnlyDictionary<string, IValidation> _validators = new Dictionary<string, IValidation>
    {
        { nameof(GenerateTestsAdvancedViewModel.TestClassName), new TestClassNameValidation() },
        { nameof(GenerateTestsAdvancedViewModel.TestClassNamespace), new TestClassNamespaceValidation() },
        { nameof(GenerateTestsAdvancedViewModel.TestFileName), new TestFileNameValidation() }
    };

    private readonly IReadOnlyDictionary<string, Control> _propertyControls;
    
    private readonly HashSet<string> _invalidProperties = [];
    
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

        _propertyControls = new Dictionary<string, Control>
        {
            { nameof(GenerateTestsAdvancedViewModel.TestClassName), TestClassNameInput },
            { nameof(GenerateTestsAdvancedViewModel.TestClassNamespace), TestClassNamespaceInput },
            { nameof(GenerateTestsAdvancedViewModel.TestFileName), TestFileNameInput }
        };
        
        _viewModel.PropertyChanged+= ValidateViewModelOnPropertyChanged;
    }

    private void ValidateViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (!_validators.TryGetValue(args.PropertyName, out var validation))
            return;

        object value = typeof(GenerateTestsAdvancedViewModel).GetProperty(args.PropertyName)!.GetValue(_viewModel);

        var element = _propertyControls[args.PropertyName];

        if (validation.Validate(value))
        {
            _invalidProperties.Remove(args.PropertyName);

            element.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x99, 0x99, 0x99));
        }
        else
        {
            _invalidProperties.Add(args.PropertyName);

            element.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        ContinueButton.IsEnabled = _invalidProperties.Count == 0;
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        InitializeTestProjectComboBox();
        InitializeDiagnosticsPanel();
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

        var orderedMethods = _viewModel.TestMethodMap
            .OrderBy(x => x.Key.Symbol.Kind)
            .ThenByDescending(x => x.Key.Symbol.GetEffectiveAccessibility());

        foreach (var (testMethod, enabled) in orderedMethods)
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
}
