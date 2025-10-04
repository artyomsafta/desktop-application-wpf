using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherUpdateService
{
    private WpfAppDbContext _context;
    private string _teacherNewName;
    private string _teacherNewSurname;
    private TeacherDto _selectedTeacher;

    public TeacherUpdateService(string name, string surname, TeacherDto selectedTeacher, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _teacherNewName = name;
        _teacherNewSurname = surname;
        _selectedTeacher = selectedTeacher;
    }

    public TeacherUpdateService(string name, string surname, TeacherDto selectedTeacher)
    {
        _context = new WpfAppDbContext();
        _teacherNewName = name;
        _teacherNewSurname = surname;
        _selectedTeacher = selectedTeacher;
    }

    public void UpdateTeacher()
    {
        var teacherFullName = _selectedTeacher.Name + " " + _selectedTeacher.Surname;
        var teacher = _context.Teachers
            .FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        teacher.Name = _teacherNewName;
        teacher.Surname = _teacherNewSurname;

        _context.SaveChanges();
    }
}
