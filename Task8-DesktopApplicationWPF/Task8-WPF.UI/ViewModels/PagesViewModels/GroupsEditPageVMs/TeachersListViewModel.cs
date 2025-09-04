using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels.PagesViewModels.GroupsEditPageVMs;

public class TeachersListViewModel : BaseViewModel
{
    private TeachersListService _teachersService;

    public ObservableCollection<TeacherDto> Teachers { get; } = new();

    public TeachersListViewModel()
    {
        _teachersService = new TeachersListService();
        LoadList();
    }

    private void LoadList()
    {
        Teachers.Clear();

        var teachersDtos = _teachersService.GetTeachersList();
        
        var sortedTeachersDtos = teachersDtos
            .OrderBy(dto => dto.Name)
            .ToList();

        foreach (var sortedTeachersDto in sortedTeachersDtos)
        {
            var teacherForListViewModel = new TeacherDto
            {
                Name = sortedTeachersDto.Name,
                Surname = sortedTeachersDto.Surname
            };

            Teachers.Add(teacherForListViewModel);
        }
    }
}
