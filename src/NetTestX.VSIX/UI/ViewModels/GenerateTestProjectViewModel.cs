﻿using System;
using System.ComponentModel;
using NetTestX.Common;
using NetTestX.VSIX.UI.Models;

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
            PropertyChanged?.Invoke(this, new(nameof(ProjectName)));
        }
    }

    public string ProjectDirectory
    {
        get => model.ProjectDirectory;
        set
        {
            model.ProjectDirectory = value;
            PropertyChanged?.Invoke(this, new(nameof(ProjectDirectory)));
        }
    }

    public string TestFramework
    {
        get => model.TestFramework.ToString();
        set
        {
            model.TestFramework = GetTestFramework(value);
            PropertyChanged?.Invoke(this, new(nameof(TestFramework)));
        }
    }

    public string MockingLibrary
    {
        get => model.MockingLibrary.ToString();
        set
        {
            model.MockingLibrary = GetMockingLibrary(value);
            PropertyChanged?.Invoke(this, new(nameof(MockingLibrary)));
        }
    }

    public bool GenerateInternalsVisibleTo
    {
        get => model.GenerateInternalsVisibleTo;
        set
        {
            model.GenerateInternalsVisibleTo = value;
            PropertyChanged?.Invoke(this, new(nameof(GenerateInternalsVisibleTo)));
        }
    }

    private static TestFramework GetTestFramework(string value) => value switch
    {
        "XUnit" => Common.TestFramework.XUnit,
        "NUnit" => Common.TestFramework.NUnit,
        "MSTest" => Common.TestFramework.MSTest,
        _ => throw new ArgumentOutOfRangeException()
    };

    private static MockingLibrary GetMockingLibrary(string value) => value switch
    {
        "NSubstitute" => Common.MockingLibrary.NSubstitute,
        "FakeItEasy" => Common.MockingLibrary.FakeItEasy,
        "Moq" => Common.MockingLibrary.Moq,
        _ => throw new ArgumentOutOfRangeException()
    };
}
