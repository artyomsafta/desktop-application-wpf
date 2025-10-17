using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels.DtoListsViewModels;

public class TeachersListViewModel : BaseViewModel
{
    private DtoListsService _dtoListsService;
    public ObservableCollection<TeacherDto> Teachers { get; } = new();

    public TeachersListViewModel()
    {
        _dtoListsService = new DtoListsService();
        LoadList();
    }

    private void LoadList()
    {
        Teachers.Clear();
        var teachersDtos = _dtoListsService.GetTeachersList();        
        var sortedTeachersDtos = teachersDtos
            .OrderBy(dto => dto.Name)
            .ToList();

        foreach (var sortedTeachersDto in sortedTeachersDtos)
        {
            var teacherForListViewModel = new TeacherDto
            {
                TeacherId = sortedTeachersDto.TeacherId,
                Name = sortedTeachersDto.Name,
                Surname = sortedTeachersDto.Surname
            };

            Teachers.Add(teacherForListViewModel);
        }
    }
}
