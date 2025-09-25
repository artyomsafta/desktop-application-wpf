using CommunityToolkit.Mvvm.Input;
using Task8_WPF.BAL.Services.StudentsServices;
using Task8_WPF.UI.ViewModels.PagesViewModels.StudentsEditPageVMs;

namespace Task8_WPF.UI.ViewModels;

public class StudentsEditViewModel : BaseViewModel
{
    private string _newStudentName;
    public string NewStudentName
    {
        get => _newStudentName;
        set 
        { 
            _newStudentName = value;
            OnPropertyChanged();
            AddNewStudentCommand.NotifyCanExecuteChanged();
        }
    }

    private string _newStudentSurname;
    public string NewStudentSurname
    {
        get => _newStudentSurname;
        set
        {
            _newStudentSurname = value;
            OnPropertyChanged();
            AddNewStudentCommand.NotifyCanExecuteChanged();
        }
    }

    public CascadeComboboxViewModel CascadeComboboxViewModel { get; }
    public IRelayCommand AddNewStudentCommand { get; }

    public StudentsEditViewModel()
    {
        CascadeComboboxViewModel = new CascadeComboboxViewModel();

        AddNewStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new StudentAddEntryService(_newStudentName, _newStudentSurname, CascadeComboboxViewModel.SelectedGroup.GroupName),
                s => s.AddStudentEntry(),
                $"student has been added."
            ),
            () => CanExecuteOperation(CascadeComboboxViewModel.SelectedGroup, _newStudentName, _newStudentSurname)
        );
    }
}
