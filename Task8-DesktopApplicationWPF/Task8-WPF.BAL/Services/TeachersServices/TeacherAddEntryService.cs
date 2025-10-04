using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherAddEntryService
{
    private WpfAppDbContext _context;
    private TeacherDto _newTeacherEntry;

    public TeacherAddEntryService(string name, string surname, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _newTeacherEntry = new TeacherDto()
        {
            Name = name,
            Surname = surname
        };
    }

    public TeacherAddEntryService(string name, string surname)
    {
        _context = new WpfAppDbContext();
        _newTeacherEntry = new TeacherDto() 
        {
            Name = name,
            Surname = surname
        };
    }

    public void AddTeacherEntry()
    {
        _context.Teachers.Add(new Teacher
        {
            Id = Guid.NewGuid(),
            Name = _newTeacherEntry.Name,
            Surname = _newTeacherEntry.Surname
        });

        _context.SaveChanges();
    }
}
