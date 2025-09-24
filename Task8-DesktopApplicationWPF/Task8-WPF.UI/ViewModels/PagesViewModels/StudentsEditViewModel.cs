using Task8_WPF.UI.ViewModels.PagesViewModels.StudentsEditPageVMs;

namespace Task8_WPF.UI.ViewModels;

public class StudentsEditViewModel : BaseViewModel
{
    public CascadeComboboxViewModel CascadeComboboxViewModel { get; }

    public StudentsEditViewModel()
    {
        CascadeComboboxViewModel = new CascadeComboboxViewModel();
    }
}
