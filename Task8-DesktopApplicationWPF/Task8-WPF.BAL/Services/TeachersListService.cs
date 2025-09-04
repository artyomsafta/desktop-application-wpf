using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services;

public class TeachersListService
{
    private WpfAppDbContext _context;

    public TeachersListService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public TeachersListService()
    {
        _context = new WpfAppDbContext();
    }

    public List<TeacherDto> GetTeachersList()
    {
        var teachersList = _context.Teachers
            .ToList();

        return teachersList.Select(t => new TeacherDto
        {
            Name = t.Name,
            Surname = t.Surname,
        }).ToList();
    }
}
