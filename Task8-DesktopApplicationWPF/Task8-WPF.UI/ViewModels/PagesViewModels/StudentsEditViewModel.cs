using CommunityToolkit.Mvvm.Input;
using Task8_WPF.BAL.Dto.TreeDtos;
using Task8_WPF.BAL.Services;
using Task8_WPF.UI.ViewModels.PagesViewModels.StudentsEditPageVMs;

namespace Task8_WPF.UI.ViewModels;

public class StudentsEditViewModel : BaseViewModel
{
    private StudentsService _studentsService;

    private StudentsTreeDto _selectedStudent;
    public StudentsTreeDto SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            _selectedStudent = value;
            OnPropertyChanged();
            UpdateStudentCommand.NotifyCanExecuteChanged();
            DeleteStudentCommand.NotifyCanExecuteChanged();
        }
    }

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
        _studentsService = new StudentsService();
        CascadeComboboxViewModel = new CascadeComboboxViewModel();

        AddNewStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => _studentsService,
                s => s.AddStudentEntry(_newStudentName, _newStudentSurname, CascadeComboboxViewModel.SelectedGroup.GroupName),
                $"student has been added."
            ),
            () => CanExecuteOperation(CascadeComboboxViewModel.SelectedGroup, _newStudentName, _newStudentSurname)
        );

        UpdateStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => _studentsService,
                s => s.UpdateStudent(
                    _studentNameToUpdate,
                    _studentSurnameToUpdate,
                    CascadeComboboxViewModel.SelectedGroup.GroupName,
                    _selectedStudent.FullName
                    ),
                $"student's data has been updated."
            ),
            () => CanExecuteOperation(_selectedStudent, _studentNameToUpdate, _studentSurnameToUpdate)
        );

        DeleteStudentCommand = new RelayCommand(
            () => ExecuteOperation(
                () => _studentsService,
                s => s.DeleteStudent(
                    CascadeComboboxViewModel.SelectedGroup.GroupName,
                    _selectedStudent.FullName
                    ),
                $"student has been deleted."
            ),
            () => CanExecuteOperation(_selectedStudent)
        );
    }
}
