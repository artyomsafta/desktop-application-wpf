using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.StudentsServices;

public class StudentDeleteService
{
    private WpfAppDbContext _context;

    public StudentDeleteService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public StudentDeleteService()
    {
        _context = new WpfAppDbContext();
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
}
