namespace Task8_WPF.UI.ViewModels;

public class DefaultViewModel : BaseViewModel
{
    private string _welcomeMessage = "Welcome to the homepage!";
    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged();
        }
    }

    public DefaultViewModel()
    {

    }
}
