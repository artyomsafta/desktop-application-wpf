using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel _currentPageViewModel;

    public BaseViewModel CurrentPageViewModel
    {
        get => _currentPageViewModel;
        set
        { 
            _currentPageViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand NavigateToDefaultCommand { get; }
    public ICommand NavigateToEditCommand { get; } // Это код для следующей страницы по заданию

    public MainViewModel()
    {
        NavigateToDefaultCommand = new RelayCommand(() => CurrentPageViewModel = new DefaultViewModel());
        NavigateToEditCommand = new RelayCommand(() => CurrentPageViewModel = new EditViewModel());
        // TODO: допиши тут все необходимые команды навигации для остальных страниц по заданию!

        CurrentPageViewModel = new DefaultViewModel();
    }
}
