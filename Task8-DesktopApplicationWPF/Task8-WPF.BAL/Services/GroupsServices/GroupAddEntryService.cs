using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupAddEntryService
{
    private WpfAppDbContext _context;

    public GroupAddEntryService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupAddEntryService()
    {
        _context = new WpfAppDbContext();
    }

    public void AddGroupEntry(string groupName, CourseDto selectedCourse, TeacherDto selectedTeacher)
    {
        var courseName = selectedCourse.CourseName;
        var teacherFullName = $"{selectedTeacher.Name} {selectedTeacher.Surname}";

        if (_context.Groups.Any(g => g.Name == groupName))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var course = _context.Courses.FirstOrDefault(c => c.Name == courseName);

        if (course is null)
        {
            throw new Exception($"Course '{courseName}' not found!");
        }

        var teacher = _context.Teachers.FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        _context.Groups.Add(new Group
        {
            Id = Guid.NewGuid(),
            Name = groupName,
            CourseId = course.Id,
            TeacherId = teacher.Id
        });

        _context.SaveChanges();
    }
}
