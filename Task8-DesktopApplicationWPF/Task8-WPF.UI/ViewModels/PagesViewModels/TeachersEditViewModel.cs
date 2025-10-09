using CommunityToolkit.Mvvm.Input;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.TeachersServices;
using Task8_WPF.UI.ViewModels.DtoListsViewModels;

namespace Task8_WPF.UI.ViewModels;

public class TeachersEditViewModel : BaseViewModel
{
    private TeacherDto _selectedTeacher;
    public TeacherDto SelectedTeacher
    {
        get => _selectedTeacher;
        set 
        { 
            _selectedTeacher = value;
            OnPropertyChanged();
            UpdateTeacherCommand.NotifyCanExecuteChanged();
            DeleteTeacherCommand.NotifyCanExecuteChanged();
        }
    }

    private string _newTeacherName;
    public string NewTeacherName
    {
        get => _newTeacherName;
        set
        {
            _newTeacherName = value;
            OnPropertyChanged();
            AddNewTeacherCommand.NotifyCanExecuteChanged();
        }
    }

    private string _newTeacherSurname;
    public string NewTeacherSurname
    {
        get => _newTeacherSurname;
        set
        {
            _newTeacherSurname = value;
            OnPropertyChanged();
            AddNewTeacherCommand.NotifyCanExecuteChanged();
        }
    }

    private string _teacherNameToUpdate;
    public string TeacherNameToUpdate
    {
        get => _teacherNameToUpdate;
        set
        {
            _teacherNameToUpdate = value;
            OnPropertyChanged();
            UpdateTeacherCommand.NotifyCanExecuteChanged();
        }
    }

    private string _teacherSurnameToUpdate;
    public string TeacherSurnameToUpdate
    {
        get => _teacherSurnameToUpdate;
        set
        {
            _teacherSurnameToUpdate = value;
            OnPropertyChanged();
            UpdateTeacherCommand.NotifyCanExecuteChanged();
        }
    }

    public TeachersListViewModel TeachersListViewModel { get; }
    public IRelayCommand AddNewTeacherCommand { get; }
    public IRelayCommand UpdateTeacherCommand { get; }
    public IRelayCommand DeleteTeacherCommand { get; }

    public TeachersEditViewModel()
    {
        TeachersListViewModel = new TeachersListViewModel();

        AddNewTeacherCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new TeacherAddEntryService(),
                t => t.AddTeacherEntry(_newTeacherName, _newTeacherSurname),
                $"teacher has been added."
            ),
            () => CanExecuteOperation(_newTeacherName, _newTeacherSurname)
        );

        UpdateTeacherCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new TeacherUpdateService(),
                t => t.UpdateTeacher(_teacherNameToUpdate, _teacherSurnameToUpdate, _selectedTeacher),
                $"teacher's data has been updated."
            ),
            () => CanExecuteOperation(_selectedTeacher, _teacherNameToUpdate, _teacherSurnameToUpdate)
        );

        DeleteTeacherCommand = new RelayCommand(
            () => ExecuteOperation(
                () => new TeacherDeleteService(),
                t => t.DeleteTeacher(_selectedTeacher),
                $"teacher has been deleted."
            ),
            () => CanExecuteOperation(_selectedTeacher)
        );
    }
}
