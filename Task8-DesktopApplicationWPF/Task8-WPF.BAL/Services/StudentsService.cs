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

    public void AddStudentEntry(string name, string surname, string selectedGroupName)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group '{selectedGroupName}' not found!");
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

    public void DeleteStudent(string selectedGroupName, string selectedStudentFullName)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group '{selectedGroupName}' not found!");
        }

        var student = _context.Students
            .Where(g => g.GroupId == group.Id)
            .FirstOrDefault(s => s.Name + " " + s.Surname == selectedStudentFullName);

        if (student is null)
        {
            throw new Exception($"Student '{selectedStudentFullName}' not found!");
        }

        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    public void UpdateStudent(string name, string surname, string selectedGroupName, string selectedStudentFullName)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group '{selectedGroupName}' not found!");
        }

        var student = _context.Students
            .Where(g => g.GroupId == group.Id)
            .FirstOrDefault(s => s.Name + " " + s.Surname == selectedStudentFullName);

        if (student is null)
        {
            throw new Exception($"Student '{selectedStudentFullName}' not found!");
        }

        student.Name = name.Trim();
        student.Surname = surname.Trim();

        _context.SaveChanges();
    }
}
