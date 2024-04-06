using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using NetTestX.VSIX.Options.Validation;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.ViewModels;
using Control = System.Windows.Controls.Control;

namespace NetTestX.VSIX.UI.Views;

public partial class GenerateTestProjectView
{
    private static readonly IReadOnlyDictionary<string, IValidation> _validators = new Dictionary<string, IValidation>
    {
        { nameof(GenerateTestProjectViewModel.ProjectName), new ProjectNameValidation() }
    };

    private readonly IReadOnlyDictionary<string, Control> _propertyControls;
    
    private readonly HashSet<string> _invalidProperties = [];
    
    private readonly GenerateTestProjectViewModel _viewModel;

    public GenerateTestProjectView(GenerateTestProjectModel model)
    {
        DataContext = _viewModel = new(model);

        InitializeComponent();
        
        _propertyControls = new Dictionary<string, Control>
        {
            { nameof(GenerateTestProjectViewModel.ProjectName), ProjectNameInput }
        };
     
        _viewModel.PropertyChanged += ValidateViewModelOnPropertyChanged;
    }

    private void ValidateViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (!_validators.TryGetValue(args.PropertyName, out var validation))
            return;

        object value = typeof(GenerateTestProjectViewModel).GetProperty(args.PropertyName)!.GetValue(_viewModel);

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
