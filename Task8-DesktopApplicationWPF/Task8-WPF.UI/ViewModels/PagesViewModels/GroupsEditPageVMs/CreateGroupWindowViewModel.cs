using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.GroupsServices;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class CreateGroupWindowViewModel : BaseViewModel
{
    private GroupAddEntryService _groupAddEntryService;

    private string _groupName;
    public string GroupName
    {
        get => _groupName;
        set
        {
            _groupName = value;
            OnPropertyChanged();
            OkCommand.NotifyCanExecuteChanged();
        }
    }

    private CourseDto _selectedCourse;
    public CourseDto SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            _selectedCourse = value;
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
    public CoursesListViewModel CoursesListViewModel { get; }
    public TeachersListViewModel TeachersListViewModel { get; }

    public CreateGroupWindowViewModel()
    {
        CoursesListViewModel = new CoursesListViewModel();
        TeachersListViewModel = new TeachersListViewModel();
        OkCommand = new RelayCommand(ExecuteOk, CanExecuteOk);
        CancelCommand = new RelayCommand<Window>(ExecuteCancel);
    }

    private bool CanExecuteOk()
    {
        return !string.IsNullOrWhiteSpace(GroupName)
            && SelectedCourse is not null
            && SelectedTeacher is not null;
    }

    private void ExecuteOk()
    {
        try
        {
            _groupAddEntryService = new GroupAddEntryService(_groupName, SelectedCourse, SelectedTeacher);
            _groupAddEntryService.AddGroupEntry();
            MessageBox.Show($"Operation successful!\nNew group name: {GroupName},\ncourse: {SelectedCourse.CourseName},\nteacher:{SelectedTeacher.Name} {SelectedTeacher.Surname}.");
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
