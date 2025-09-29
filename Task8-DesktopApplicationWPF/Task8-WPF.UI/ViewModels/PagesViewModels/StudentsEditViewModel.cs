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

    private string _studentNameToUpdate;
    public string StudentNameToUpdate
    {
        get => _studentNameToUpdate;
        set
        {
            _studentNameToUpdate = value;
            OnPropertyChanged();
            UpdateStudentCommand.NotifyCanExecuteChanged();
        }
    }

    private string _studentSurnameToUpdate;
    public string StudentSurnameToUpdate
    {
        get => _studentSurnameToUpdate;
        set
        {
            _studentSurnameToUpdate = value;
            OnPropertyChanged();
            UpdateStudentCommand.NotifyCanExecuteChanged();
        }
    }

    public CascadeComboboxViewModel CascadeComboboxViewModel { get; }
    public IRelayCommand AddNewStudentCommand { get; }
    public IRelayCommand UpdateStudentCommand { get; }
    public IRelayCommand DeleteStudentCommand { get; }

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

        UpdateStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new StudentUpdateService(
                    _studentNameToUpdate,
                    _studentSurnameToUpdate,
                    CascadeComboboxViewModel.SelectedGroup.GroupName,
                    CascadeComboboxViewModel.SelectedStudent.FullName
                    ),
                s => s.UpdateStudent(),
                $"student's data has been updated."
            ),
            () => CanExecuteOperation(CascadeComboboxViewModel.SelectedStudent, _studentNameToUpdate, _studentSurnameToUpdate)
        );

        DeleteStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new StudentDeleteService(
                    CascadeComboboxViewModel.SelectedGroup.GroupName,
                    CascadeComboboxViewModel.SelectedStudent.FullName
                    ),
                s => s.DeleteStudent(),
                $"student has been deleted."
            )/*,
            () => CanExecuteOperation(CascadeComboboxViewModel.SelectedStudent)*/ //ТУТ ПРОБЛЕМА!!!
        );
    }
}
