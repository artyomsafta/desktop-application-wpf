using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.StudentsServices;

public class StudentAddEntryService
{
    private WpfAppDbContext _context;
    private StudentDto _newStudentEntry;

    public StudentAddEntryService(string name, string surname, string selectedGroupName, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _newStudentEntry = new StudentDto()
        {
            Name = name,
            Surname = surname,
            GroupName = selectedGroupName
        };
    }

    public StudentAddEntryService(string name, string surname, string selectedGroupName)
    {
        _context = new WpfAppDbContext();
        _newStudentEntry = new StudentDto()
        {
            Name = name,
            Surname = surname,
            GroupName = selectedGroupName
        };
    }

    public void AddStudentEntry()
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == _newStudentEntry.GroupName);

        if (group is null) 
        {
            throw new Exception($"Group '{_newStudentEntry.GroupName}' not found!");
        }

        _context.Students.Add(new Student 
        { 
            Id = Guid.NewGuid(),
            Name = _newStudentEntry.Name,
            Surname = _newStudentEntry.Surname,
            GroupId = group.Id
        });

        _context.SaveChanges();
    }
}
