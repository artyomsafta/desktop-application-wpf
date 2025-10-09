using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherDeleteService
{
    private WpfAppDbContext _context;

    public TeacherDeleteService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public TeacherDeleteService()
    {
        _context = new WpfAppDbContext();
    }

    public void DeleteTeacher(TeacherDto selectedTeacher)
    {
        var teacherFullName = selectedTeacher.Name + " " + selectedTeacher.Surname;
        var teacher = _context.Teachers
            .Include(g => g.Groups)
            .FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        if (teacher.Groups.Any())
        {
            throw new Exception($"Teacher '{teacherFullName}' cannot be deleted because he still has groups!");
        }

        _context.Teachers.Remove(teacher);
        _context.SaveChanges();
    }
}
