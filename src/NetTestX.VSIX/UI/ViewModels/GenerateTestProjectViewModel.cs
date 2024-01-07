﻿using NetTestX.Common;
using NetTestX.VSIX.Models;
using System;
using System.ComponentModel;

namespace NetTestX.VSIX.UI.ViewModels;

public class GenerateTestProjectViewModel(GenerateTestProjectModel model) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
 
    public string ProjectName
    {
        get => model.ProjectName;
        set
        {
            model.ProjectName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectName)));
        }
    }

    public string ProjectDirectory
    {
        get => model.ProjectDirectory;
        set
        {
            model.ProjectDirectory = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectDirectory)));
        }
    }

    public string TestFramework
    {
        get => model.TestFramework.ToString();
        set
        {
            model.TestFramework = GetTestFramework(value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestFramework)));
        }
    }

    public string MockingLibrary
    {
        get => model.MockingLibrary.ToString();
        set
        {
            model.MockingLibrary = GetMockingLibrary(value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MockingLibrary)));
        }
    }

    private static TestFramework GetTestFramework(string value) => value switch
    {
        "XUnit" => Common.TestFramework.XUnit,
        "NUnit" => Common.TestFramework.NUnit,
        _ => throw new ArgumentOutOfRangeException()
    };

    private static MockingLibrary GetMockingLibrary(string value) => value switch
    {
        "NSubstitute" => Common.MockingLibrary.NSubstitute,
        _ => throw new ArgumentOutOfRangeException()
    };
}