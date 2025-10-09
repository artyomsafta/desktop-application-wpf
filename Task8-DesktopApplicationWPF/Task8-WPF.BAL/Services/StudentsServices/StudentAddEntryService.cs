using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.StudentsServices;

public class StudentAddEntryService
{
    private WpfAppDbContext _context;

    public StudentAddEntryService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public StudentAddEntryService()
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
            Name = name,
            Surname = surname,
            GroupId = group.Id
        });

        _context.SaveChanges();
    }
}
