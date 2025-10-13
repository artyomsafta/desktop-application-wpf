using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class CreateGroupWindowViewModel : BaseViewModel
{
    private GroupsService _groupsService;

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
            _groupsService = new GroupsService();
            _groupsService.AddGroupEntry(_groupName, SelectedCourse, SelectedTeacher);
            MessageBox.Show($"Operation successful!\nNew group name: {GroupName},\n" +
                            $"course: {SelectedCourse.CourseName},\n" +
                            $"teacher:{SelectedTeacher.Name} {SelectedTeacher.Surname}.");
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
