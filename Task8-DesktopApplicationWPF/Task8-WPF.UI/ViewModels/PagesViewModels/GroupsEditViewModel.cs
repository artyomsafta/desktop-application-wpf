using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.FileServices;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;
using Task8_WPF.UI.ViewModels.SystemViewModels;
using Task8_WPF.UI.Views;

namespace Task8_WPF.UI.ViewModels;

public class GroupsEditViewModel : BaseViewModel
{
    private GroupDeleteService _groupsDeleteService;
    private ImportFileService _importFileService;
    private ExportFileService _exportFileService;
    private CreateDocxFileService _createDocxFileService;

    private GroupDto _selectedGroup;

    public GroupDto SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            _selectedGroup = value;
            OnPropertyChanged();
            DeleteGroupCommand.NotifyCanExecuteChanged();
        }
    }

    private GroupDto _selectedImportGroup;

    public GroupDto SelectedImportGroup
    {
        get => _selectedImportGroup;
        set
        {
            _selectedImportGroup = value;
            OnPropertyChanged();
            ImportStudentsCommand.NotifyCanExecuteChanged();
        }
    }

    private GroupDto _selectedExportGroup;

    public GroupDto SelectedExportGroup
    {
        get => _selectedExportGroup;
        set
        {
            _selectedExportGroup = value;
            OnPropertyChanged();
            ExportStudentsCommand.NotifyCanExecuteChanged();
        }
    }

    private GroupDto _selectedSaveToFileGroup;

    public GroupDto SelectedSaveToFileGroup
    {
        get => _selectedSaveToFileGroup;
        set
        {
            _selectedSaveToFileGroup = value;
            OnPropertyChanged();
            SaveStudentsToDocxCommand.NotifyCanExecuteChanged();
        }
    }

    public GroupsListViewModel GroupsListViewModel { get; }
    public ICommand OpenCreateGroupWindowCommand { get; }
    public ICommand OpenRenameGroupWindowCommand {  get; }
    public IRelayCommand DeleteGroupCommand { get; }
    public ICommand OpenUpdateTeacherWindowCommand { get; }
    public FileDialogViewModel FileDialogViewModel { get; }
    public IRelayCommand ImportStudentsCommand { get; }
    public IRelayCommand ExportStudentsCommand { get; }
    public IRelayCommand SaveStudentsToDocxCommand {  get; }

    public GroupsEditViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
        OpenCreateGroupWindowCommand = new RelayCommand(OpenCreateGroupWindow);
        OpenRenameGroupWindowCommand = new RelayCommand(OpenRenameGroupWindow);
        DeleteGroupCommand = new RelayCommand(DeleteGroup, CanExecuteDeleteGroup);
        OpenUpdateTeacherWindowCommand = new RelayCommand(OpenUpdateTeacherWindow);
        FileDialogViewModel = new FileDialogViewModel();
        ImportStudentsCommand = new RelayCommand(ImportStudents, CanExecuteImportStudents);
        ExportStudentsCommand = new RelayCommand(ExportStudents, CanExecuteExportStudents);
        SaveStudentsToDocxCommand = new RelayCommand(SaveStudentsToDocx, CanExecuteSaveStudentsToFile);
    }

    private void OpenCreateGroupWindow()
    {
        var window = new CreateGroupWindowView
        {
            DataContext = new CreateGroupWindowViewModel()
        };

        window.ShowDialog();
    }

    private void OpenRenameGroupWindow()
    {
        var window = new RenameGroupWindowView
        {
            DataContext = new RenameGroupWindowViewModel()
        };

        window.ShowDialog();
    }

    private void DeleteGroup()
    {
        try
        {
            _groupsDeleteService = new GroupDeleteService(_selectedGroup);
            _groupsDeleteService.DeleteGroup();
            MessageBox.Show($"Operation successful!\nGroup: {SelectedGroup.GroupName} has been deleted.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CanExecuteDeleteGroup()
    {
        return SelectedGroup is not null;
    }

    private void OpenUpdateTeacherWindow()
    {
        var window = new UpdateTeacherWindowView
        {
            DataContext = new UpdateTeacherWindowViewModel()
        };

        window.ShowDialog();
    }

    private void ImportStudents()
    {
        try
        {
            _importFileService = new ImportFileService(_selectedImportGroup, FileDialogViewModel.SelectedFilePath);
            _importFileService.ImportStudents();
            MessageBox.Show($"Operation successful!\nGroup: {SelectedImportGroup.GroupName} has been imported.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CanExecuteImportStudents()
    {
        return SelectedImportGroup is not null
            && !string.IsNullOrWhiteSpace(FileDialogViewModel.SelectedFilePath);
    }

    private void ExportStudents()
    {
        try
        {
            _exportFileService = new ExportFileService(_selectedExportGroup, FileDialogViewModel.SavedFilePath);
            _exportFileService.ExportStudents();
            MessageBox.Show($"Operation successful!\nGroup: {SelectedExportGroup.GroupName} has been exported to file.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CanExecuteExportStudents()
    {
        return SelectedExportGroup is not null
            && !string.IsNullOrWhiteSpace(FileDialogViewModel.SavedFilePath);
    }

    private void SaveStudentsToDocx()
    {
        try
        {
            _createDocxFileService = new CreateDocxFileService(_selectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath);
            _createDocxFileService.ExportStudentsToDocx();
            MessageBox.Show($"Operation successful!\nGroup: {SelectedSaveToFileGroup.GroupName} has been exported to file.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CanExecuteSaveStudentsToFile()
    {
        return SelectedSaveToFileGroup is not null
            && !string.IsNullOrWhiteSpace(FileDialogViewModel.SelectedFolderPath);
    }
}
