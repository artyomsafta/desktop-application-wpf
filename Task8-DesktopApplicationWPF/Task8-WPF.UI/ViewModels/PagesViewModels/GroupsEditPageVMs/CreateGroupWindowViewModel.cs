namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class CreateGroupWindowViewModel : BaseViewModel
{
    public CoursesListViewModel CoursesListViewModel { get; }
    public TeachersListViewModel TeachersListViewModel { get; }

    public CreateGroupWindowViewModel()
    {
        CoursesListViewModel = new CoursesListViewModel();
        TeachersListViewModel = new TeachersListViewModel();
    }
}
