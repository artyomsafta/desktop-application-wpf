using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.DtoListsServices;

public class CoursesListService
{
    private WpfAppDbContext _context;

    public CoursesListService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public CoursesListService()
    {
        _context = new WpfAppDbContext();
    }

    public List<CourseDto> GetCoursesList()
    {
        var coursesList = _context.Courses
            .ToList();

        return coursesList.Select(c => new CourseDto
        {
            CourseName = c.Name,
            Description = c.Description
        }).ToList();
    }
}

