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
            SaveStudentsToPdfCommand.NotifyCanExecuteChanged();
        }
    }

    public GroupsListViewModel GroupsListViewModel { get; }
    public FileDialogViewModel FileDialogViewModel { get; }
    public ICommand OpenCreateGroupWindowCommand { get; }
    public ICommand OpenRenameGroupWindowCommand {  get; }
    public ICommand OpenUpdateTeacherWindowCommand { get; }
    public IRelayCommand DeleteGroupCommand { get; }
    public IRelayCommand ImportStudentsCommand { get; }
    public IRelayCommand ExportStudentsCommand { get; }
    public IRelayCommand SaveStudentsToDocxCommand {  get; }
    public IRelayCommand SaveStudentsToPdfCommand { get; }

    public GroupsEditViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
        FileDialogViewModel = new FileDialogViewModel();

        OpenCreateGroupWindowCommand = new RelayCommand(OpenSubWindow<CreateGroupWindowView, CreateGroupWindowViewModel>);
        OpenRenameGroupWindowCommand = new RelayCommand(OpenSubWindow<RenameGroupWindowView, RenameGroupWindowViewModel>);
        OpenUpdateTeacherWindowCommand = new RelayCommand(OpenSubWindow<UpdateTeacherWindowView, UpdateTeacherWindowViewModel>);

        DeleteGroupCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new GroupDeleteService(_selectedGroup),
                s => s.DeleteGroup(),
                $"{SelectedGroup.GroupName} has been deleted."
            ),
            () => CanExecuteOperation(SelectedGroup)
        );

        ImportStudentsCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new ImportFileService(_selectedImportGroup, FileDialogViewModel.SelectedFilePath),
                s => s.ImportStudents(),
                $"{SelectedImportGroup.GroupName} has been imported."
            ),
            () => CanExecuteOperation(SelectedImportGroup, FileDialogViewModel.SelectedFilePath)
        );

        ExportStudentsCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new ExportFileService(_selectedExportGroup, FileDialogViewModel.SavedFilePath),
                s => s.ExportStudents(),
                $"{SelectedExportGroup.GroupName} has been exported to file."
            ),
            () => CanExecuteOperation(SelectedExportGroup, FileDialogViewModel.SavedFilePath)
        );

        SaveStudentsToDocxCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new CreateDocxFileService(_selectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath),
                s => s.ExportStudentsToDocx(),
                $"{SelectedSaveToFileGroup.GroupName} has been exported to file."
            ),
            () => CanExecuteOperation(SelectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath)
        );

        SaveStudentsToPdfCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new CreatePdfFileService(_selectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath),
                s => s.ExportStudentsToPdf(),
                $"{SelectedSaveToFileGroup.GroupName} has been exported to file."
            ),
            () => CanExecuteOperation(SelectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath)
        );
    }

    private void OpenSubWindow<TWindow, TViewModel>()
        where TWindow : Window, new()
        where TViewModel : class, new()
    {
        var window = new TWindow
        {
            DataContext = new TViewModel()
        };

        window.ShowDialog();
    }

    private void ExecuteOperation<TService>(
        Func<TService> serviceInstance,
        Action<TService> serviceAction,
        string successMessage)
    {
        try
        {
            var service = serviceInstance();
            serviceAction(service);
            MessageBox.Show($"Operation successful!\nGroup: " + successMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CanExecuteOperation<TSelectedObj>(TSelectedObj selectedObj)
        where TSelectedObj : class
    {
        return selectedObj is not null;
    }

    private bool CanExecuteOperation<TSelectedObj>(TSelectedObj selectedObj, string path)
        where TSelectedObj : class
    {
        return selectedObj is not null
            && !string.IsNullOrWhiteSpace(path);
    }
}
