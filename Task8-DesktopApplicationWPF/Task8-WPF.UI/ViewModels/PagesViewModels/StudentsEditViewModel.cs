using Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

namespace Task8_WPF.UI.ViewModels;

public class StudentsEditViewModel : BaseViewModel
{
    public CoursesListViewModel CoursesListViewModel { get; }
    public GroupsListViewModel GroupsListViewModel { get; }

    public StudentsEditViewModel()
    {
        CoursesListViewModel = new CoursesListViewModel();
        GroupsListViewModel = new GroupsListViewModel();
    }
}
