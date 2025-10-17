using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services;

namespace Task8_WPF.UI.ViewModels.DtoListsViewModels;

public class CoursesListViewModel : BaseViewModel
{
    private DtoListsService _dtoListsService;

    public ObservableCollection<CourseDto> Courses { get; } = new();

    public CoursesListViewModel()
    {
        _dtoListsService = new DtoListsService();
        LoadList();
    }

    private void LoadList()
    {
        Courses.Clear();
        var coursesDtos = _dtoListsService.GetCoursesList();
        var sortedCoursesDtos = coursesDtos
            .OrderBy(dto => dto.CourseName)
            .ToList();

        foreach (var sortedCoursesDto in sortedCoursesDtos)
        {
            var courseForListViewModel = new CourseDto
            {
                CourseId = sortedCoursesDto.CourseId,
                CourseName = sortedCoursesDto.CourseName,
                Description = sortedCoursesDto.Description
            };

            Courses.Add(courseForListViewModel);
        }
    }
}
