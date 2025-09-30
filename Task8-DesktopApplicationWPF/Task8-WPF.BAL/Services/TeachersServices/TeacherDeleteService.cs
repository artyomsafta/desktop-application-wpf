using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherDeleteService
{
    private WpfAppDbContext _context = new WpfAppDbContext();
    private TeacherDto _selectedTeacher;

    public TeacherDeleteService(TeacherDto selectedTeacher)
    {
        _selectedTeacher = selectedTeacher;
    }

    public void DeleteTeacher()
    {
        var teacherFullName = _selectedTeacher.Name + " " + _selectedTeacher.Surname;
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
