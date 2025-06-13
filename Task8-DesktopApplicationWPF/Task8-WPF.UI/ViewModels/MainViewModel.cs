using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

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
    public ICommand NavigateToCoursesEditCommand { get; }
    public ICommand NavigateToStudentsEditCommand { get; }
    public ICommand NavigateToTeachersEditCommand { get; }

    public MainViewModel()
    {
        NavigateToDefaultCommand = new RelayCommand(() => CurrentPageViewModel = new DefaultViewModel());
        NavigateToCoursesEditCommand = new RelayCommand(() => CurrentPageViewModel = new GroupsEditViewModel());
        NavigateToStudentsEditCommand = new RelayCommand(() => CurrentPageViewModel = new StudentsEditViewModel());
        NavigateToTeachersEditCommand = new RelayCommand(() => CurrentPageViewModel = new TeachersEditViewModel());

        CurrentPageViewModel = new DefaultViewModel();
    }
}
