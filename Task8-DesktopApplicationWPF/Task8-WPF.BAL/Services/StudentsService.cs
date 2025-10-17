using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services;

public class StudentsService
{
    private WpfAppDbContext _context;

    public StudentsService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public StudentsService()
    {
        _context = new WpfAppDbContext();
    }

    public void AddStudentEntry(string name, string surname, Guid selectedGroupId)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Id == selectedGroupId);

        if (group is null)
        {
            throw new Exception($"Group not found!");
        }

        _context.Students.Add(new Student
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Surname = surname.Trim(),
            GroupId = group.Id
        });

        _context.SaveChanges();
    }

    public void DeleteStudent(Guid selectedStudentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == selectedStudentId);

        if (student is null)
        {
            throw new Exception($"Student not found!");
        }

        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    public void UpdateStudent(string name, string surname, Guid selectedStudentId)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == selectedStudentId);

        if (student is null)
        {
            throw new Exception($"Student not found!");
        }

        student.Name = name.Trim();
        student.Surname = surname.Trim();

        _context.SaveChanges();
    }
}
