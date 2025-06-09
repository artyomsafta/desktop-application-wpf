namespace Task8_WPF.UI.ViewModels;

public class EditViewModel : BaseViewModel
{
    private string _welcomeMessage = "This is edit page";
    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged();
        }
    }

    public EditViewModel()
    {
        
    }
}
