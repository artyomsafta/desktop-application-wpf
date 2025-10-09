using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class UpdateTeacherWindowViewModel : BaseViewModel
{
    private GroupUpdateTeacherService _groupUpdateTeacherService;

    private GroupDto _selectedGroup;
    public GroupDto SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            _selectedGroup = value;
            OnPropertyChanged();
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    private TeacherDto _selectedTeacher;
    public TeacherDto SelectedTeacher
    {
        get => _selectedTeacher;
        set
        {
            _selectedTeacher = value;
            OnPropertyChanged();
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    public IRelayCommand OkCommand { get; }
    public ICommand CancelCommand { get; }
    public GroupsListViewModel GroupsListViewModel { get; }
    public TeachersListViewModel TeachersListViewModel { get; }

    public UpdateTeacherWindowViewModel()
    {
        GroupsListViewModel = new GroupsListViewModel();
        TeachersListViewModel = new TeachersListViewModel();
        OkCommand = new RelayCommand(ExecuteOk, CanExecuteOk);
        CancelCommand = new RelayCommand<Window>(ExecuteCancel);
    }

    private bool CanExecuteOk()
    {
        return SelectedGroup is not null
            && SelectedTeacher is not null;
    }

    private void ExecuteOk()
    {
        try
        {
            _groupUpdateTeacherService = new GroupUpdateTeacherService();
            _groupUpdateTeacherService.UpdateTeacher(_selectedGroup, SelectedTeacher);
            MessageBox.Show($"Operation successful!\nTeacher has been updated.\nNew teacher's name: {SelectedTeacher.Name} {SelectedTeacher.Surname}.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ExecuteCancel(Window window)
    {
        window?.Close();
    }
}
