using Microsoft.EntityFrameworkCore;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.TeachersServices;

public class TeacherAddEntryService
{
    private WpfAppDbContext _context;

    public TeacherAddEntryService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public TeacherAddEntryService()
    {
        _context = new WpfAppDbContext();
    }

    public void AddTeacherEntry(string name, string surname)
    {
        _context.Teachers.Add(new Teacher
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname
        });

        _context.SaveChanges();
    }
}
