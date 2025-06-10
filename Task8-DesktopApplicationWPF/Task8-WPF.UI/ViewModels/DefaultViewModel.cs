namespace Task8_WPF.UI.ViewModels;

public class DefaultViewModel : BaseViewModel
{
    public CoursesTreeViewModel CoursesTreeViewModel { get; }

    public DefaultViewModel()
    {
        CoursesTreeViewModel = new CoursesTreeViewModel();
    }
}
