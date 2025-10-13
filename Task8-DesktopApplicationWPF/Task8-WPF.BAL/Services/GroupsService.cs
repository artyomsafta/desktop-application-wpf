using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services;

public class GroupsService
{
    private WpfAppDbContext _context;

    public GroupsService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupsService()
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

    public void DeleteGroup(GroupDto selectedGroup)
    {
        var group = _context.Groups
            .Include(s => s.Students)
            .FirstOrDefault(g => g.Name == selectedGroup.GroupName);

        if (group is null)
        {
            throw new Exception($"Group {selectedGroup.GroupName} not found!");
        }

        if (group.Students.Any())
        {
            throw new Exception($"Group {selectedGroup.GroupName} cannot be deleted because it has students in it!");
        }

        _context.Groups.Remove(group);
        _context.SaveChanges();
    }

    public void RenameGroup(string groupNewName, GroupDto selectedGroup)
    {
        if (_context.Groups.Any(g => g.Name == groupNewName))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var group = _context.Groups.FirstOrDefault(g => g.Name == selectedGroup.GroupName);

        if (group is null)
        {
            throw new Exception($"Group {selectedGroup.GroupName} not found!");
        }

        group.Name = groupNewName;
        _context.SaveChanges();
    }

    public void UpdateTeacher(GroupDto selectedGroup, TeacherDto selectedTeacher)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == selectedGroup.GroupName);

        if (group is null)
        {
            throw new Exception($"Group '{selectedGroup.GroupName}' not found!");
        }

        var teacherFullName = $"{selectedTeacher.Name} {selectedTeacher.Surname}";
        var teacher = _context.Teachers.FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        group.TeacherId = teacher.Id;
        _context.SaveChanges();
    }
}
