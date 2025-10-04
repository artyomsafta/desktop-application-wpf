using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.StudentsServices;

public class StudentUpdateService
{
    private WpfAppDbContext _context;
    private string _studentNewName;
    private string _studentNewSurname;
    private string _selectedGroupName;
    private string _selectedStudentFullName;

    public StudentUpdateService(string name, string surname, string selectedGroupName, string selectedStudentFullName, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _studentNewName = name;
        _studentNewSurname = surname;
        _selectedGroupName = selectedGroupName;
        _selectedStudentFullName = selectedStudentFullName;
    }

    public StudentUpdateService(string name, string surname, string selectedGroupName, string selectedStudentFullName)
    {
        _context = new WpfAppDbContext();
        _studentNewName = name;
        _studentNewSurname = surname;
        _selectedGroupName = selectedGroupName;
        _selectedStudentFullName = selectedStudentFullName;
    }

    public void UpdateStudent()
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

        student.Name = _studentNewName;
        student.Surname = _studentNewSurname;

        _context.SaveChanges();
    }
}
