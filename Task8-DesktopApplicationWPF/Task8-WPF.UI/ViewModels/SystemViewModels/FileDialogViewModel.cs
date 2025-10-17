using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;

namespace Task8_WPF.UI.ViewModels.SystemViewModels;

public class FileDialogViewModel : BaseViewModel
{
    private string _selectedFilePath;
    public string SelectedFilePath
    {
        get => _selectedFilePath;
        set 
        { 
            _selectedFilePath = value; 
            OnPropertyChanged();
        }
    }

    private string _savedFilePath;
    public string SavedFilePath
    {
        get => _savedFilePath;
        set 
        { 
            _savedFilePath = value; 
            OnPropertyChanged(); 
        }
    }

    private string _selectedFolderPath;
    public string SelectedFolderPath
    {
        get => _selectedFolderPath;
        set
        {
            _selectedFolderPath = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenFileCommand { get; }
    public ICommand SaveFileCommand { get; }
    public ICommand OpenFolderCommand { get; }

    public FileDialogViewModel()
    {
        OpenFileCommand = new RelayCommand(OpenFile);
        SaveFileCommand = new RelayCommand(SaveFile);
        OpenFolderCommand = new RelayCommand(OpenFolder);
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select file",
            Filter = "cvs files (*.csv)|*.csv"
        };

        if (openFileDialog.ShowDialog() is true)
        {
            var selectedFile = openFileDialog.FileName;
            SelectedFilePath = selectedFile;
        }
    }

    private void SaveFile()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Title = "Save as...",
            Filter = "cvs files (*.csv)|*.csv",
            FileName = "document.csv"
        };

        if (saveFileDialog.ShowDialog() is true)
        {
            var savedFile = saveFileDialog.FileName;
            SavedFilePath = savedFile;
        }
    }

    private void OpenFolder()
    {
        var openFolderDialog = new VistaFolderBrowserDialog
        {
            Description = "Select folder to save document...",
            UseDescriptionForTitle = true
        };

        if (openFolderDialog.ShowDialog().GetValueOrDefault())
        {
            var SelectedFolder = openFolderDialog.SelectedPath;
            SelectedFolderPath = SelectedFolder;
        }
    }
}
