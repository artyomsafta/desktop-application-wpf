using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.BAL.Services.FileServices;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;
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
                () => new GroupsService(),
                s => s.DeleteGroup(_selectedGroup),
                $"Group: {SelectedGroup.GroupName} has been deleted."
            ),
            () => CanExecuteOperation(SelectedGroup)
        );

        ImportStudentsCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new ImportFileService(),
                s => s.ImportStudents(_selectedImportGroup, FileDialogViewModel.SelectedFilePath),
                $"Group: {SelectedImportGroup.GroupName} has been imported."
            ),
            () => CanExecuteOperation(SelectedImportGroup, FileDialogViewModel.SelectedFilePath)
        );

        ExportStudentsCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new ExportFileService(),
                s => s.ExportStudents(_selectedExportGroup, FileDialogViewModel.SavedFilePath),
                $"Group: {SelectedExportGroup.GroupName} has been exported to file."
            ),
            () => CanExecuteOperation(SelectedExportGroup, FileDialogViewModel.SavedFilePath)
        );

        SaveStudentsToDocxCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new CreateDocxFileService(),
                s => s.ExportStudentsToDocx(_selectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath),
                $"Group: {SelectedSaveToFileGroup.GroupName} has been exported to file."
            ),
            () => CanExecuteOperation(SelectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath)
        );

        SaveStudentsToPdfCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new CreatePdfFileService(_selectedSaveToFileGroup, FileDialogViewModel.SelectedFolderPath),
                s => s.ExportStudentsToPdf(),
                $"Group: {SelectedSaveToFileGroup.GroupName} has been exported to file."
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
}
