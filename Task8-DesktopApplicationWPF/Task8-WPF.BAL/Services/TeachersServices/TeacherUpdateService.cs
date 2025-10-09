using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherUpdateService
{
    private WpfAppDbContext _context;

    public TeacherUpdateService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public TeacherUpdateService()
    {
        _context = new WpfAppDbContext();
    }

    public void UpdateTeacher(string name, string surname, TeacherDto selectedTeacher)
    {
        var teacherFullName = selectedTeacher.Name + " " + selectedTeacher.Surname;
        var teacher = _context.Teachers
            .FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        teacher.Name = name;
        teacher.Surname = surname;

        _context.SaveChanges();
    }
}
