using System.Collections.ObjectModel;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;

namespace Task8_WPF.UI.ViewModels.DtoListsViewModels;

public class CoursesListViewModel : BaseViewModel
{
    private CoursesListService _coursesService;

    public ObservableCollection<CourseDto> Courses { get; } = new();

    public CoursesListViewModel()
    {
        _coursesService = new CoursesListService();
        LoadList();
    }

    private void LoadList()
    {
        Courses.Clear();

        var coursesDtos = _coursesService.GetCoursesList();

        var sortedCoursesDtos = coursesDtos
            .OrderBy(dto => dto.CourseName)
            .ToList();

        foreach (var sortedCoursesDto in sortedCoursesDtos)
        {
            var courseForListViewModel = new CourseDto
            {
                CourseName = sortedCoursesDto.CourseName,
                Description = sortedCoursesDto.Description
            };

            Courses.Add(courseForListViewModel);
        }
    }
}

