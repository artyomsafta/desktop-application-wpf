namespace Task8_WPF.UI.ViewModels;

public class DefaultViewModel : BaseViewModel
{
    public TreeViewModel TreeViewModel { get; }

    public DefaultViewModel()
    {
        TreeViewModel = new TreeViewModel();
    }
}
