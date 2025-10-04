using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.StudentsServices;

public class StudentDeleteService
{
    private WpfAppDbContext _context;
    private string _selectedGroupName;
    private string _selectedStudentFullName;

    public StudentDeleteService(string selectedGroupName, string selectedStudentFullName, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _selectedGroupName = selectedGroupName;
        _selectedStudentFullName = selectedStudentFullName;
    }

    public StudentDeleteService(string selectedGroupName, string selectedStudentFullName)
    {
        _context = new WpfAppDbContext();
        _selectedGroupName = selectedGroupName;
        _selectedStudentFullName = selectedStudentFullName;
    }

    public void DeleteStudent()
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == _selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group '{_selectedGroupName}' not found!");
        }

        var student = _context.Students
            .Where(g => g.GroupId == group.Id)
            .FirstOrDefault(s => s.Name + " " + s.Surname == _selectedStudentFullName);

        if (student is null)
        {
            throw new Exception($"Student '{_selectedStudentFullName}' not found!");
        }

        _context.Students.Remove(student);
        _context.SaveChanges();
    }
}
