using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Windows.Input;

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

    public ICommand OpenFileCommand { get; }
    public ICommand SaveFileCommand { get; }

    public FileDialogViewModel()
    {
        OpenFileCommand = new RelayCommand(OpenFile);
        SaveFileCommand = new RelayCommand(SaveFile);
    }

    private void OpenFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select file",
            Filter = "cvs files (*.csv)|*.csv"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string selectedFile = openFileDialog.FileName;
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

        if (saveFileDialog.ShowDialog() == true)
        {
            string savedFile = saveFileDialog.FileName;
            SavedFilePath = savedFile;
        }
    }
}
