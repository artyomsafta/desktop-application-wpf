using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services;

public class TeachersService
{
    private WpfAppDbContext _context;

    public TeachersService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public TeachersService()
    {
        _context = new WpfAppDbContext();
    }

    public void AddTeacherEntry(string name, string surname)
    {
        _context.Teachers.Add(new Teacher
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Surname = surname.Trim()
        });

        _context.SaveChanges();
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

    public void UpdateTeacher(string name, string surname, TeacherDto selectedTeacher)
    {
        var teacherFullName = selectedTeacher.Name + " " + selectedTeacher.Surname;
        var teacher = _context.Teachers
            .FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        teacher.Name = name.Trim();
        teacher.Surname = surname.Trim();

        _context.SaveChanges();
    }
}
