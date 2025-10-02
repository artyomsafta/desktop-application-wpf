using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupAddEntryService
{
    private WpfAppDbContext _context;
    private string _groupName;
    private CourseDto _selectedCourse;
    private TeacherDto _selectedTeacher;
    private GroupDto _newGroupEntry;

    public GroupAddEntryService(string groupName, CourseDto selectedCourse, TeacherDto selectedTeacher, DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
        _groupName = groupName;
        _selectedCourse = selectedCourse;
        _selectedTeacher = selectedTeacher;

        _newGroupEntry = new GroupDto()
        {
            GroupName = _groupName,
            CourseName = _selectedCourse.CourseName,
            TeacherFullName = $"{_selectedTeacher.Name} {_selectedTeacher.Surname}"
        };
    }

    public GroupAddEntryService(string groupName, CourseDto selectedCourse, TeacherDto selectedTeacher)
    {
        _context = new WpfAppDbContext();
        _groupName = groupName;
        _selectedCourse = selectedCourse;
        _selectedTeacher = selectedTeacher;

        _newGroupEntry = new GroupDto()
        {
            GroupName = _groupName,
            CourseName = _selectedCourse.CourseName,
            TeacherFullName = $"{_selectedTeacher.Name} {_selectedTeacher.Surname}"
        };
    }

    public void AddGroupEntry()
    {
        if (_context.Groups.Any(g => g.Name == _newGroupEntry.GroupName))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var course = _context.Courses.FirstOrDefault(c => c.Name == _newGroupEntry.CourseName);

        if (course is null)
        {
            throw new Exception($"Course '{_newGroupEntry.CourseName}' not found!");
        }

        var teacher = _context.Teachers.FirstOrDefault(t => t.Name + " " + t.Surname == _newGroupEntry.TeacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{_newGroupEntry.TeacherFullName}' not found!");
        }

        _context.Groups.Add(new Group
        {
            Id = Guid.NewGuid(),
            Name = _newGroupEntry.GroupName,
            CourseId = course.Id,
            TeacherId = teacher.Id
        });

        _context.SaveChanges();
    }
}
