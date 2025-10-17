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
        var newGroupName = groupName.Trim();
        var teacherFullName = $"{selectedTeacher.Name} {selectedTeacher.Surname}";

        if (_context.Groups.Any(g => g.Name.ToLower() == newGroupName.ToLower()))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var course = _context.Courses.FirstOrDefault(c => c.Id == selectedCourse.CourseId);

        if (course is null)
        {
            throw new Exception($"Course '{selectedCourse.CourseName}' not found!");
        }

        var teacher = _context.Teachers.FirstOrDefault(t => t.Id == selectedTeacher.TeacherId);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        _context.Groups.Add(new Group
        {
            Id = Guid.NewGuid(),
            Name = newGroupName,
            CourseId = course.Id,
            TeacherId = teacher.Id
        });

        _context.SaveChanges();
    }

    public void DeleteGroup(GroupDto selectedGroup)
    {
        var group = _context.Groups
            .Include(s => s.Students)
            .FirstOrDefault(g => g.Id == selectedGroup.GroupId);

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

    public void RenameGroup(string groupName, GroupDto selectedGroup)
    {
        var groupNewName = groupName.Trim();

        if (_context.Groups.Any(g => g.Name.ToLower() == groupNewName.ToLower()))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var group = _context.Groups.FirstOrDefault(g => g.Id == selectedGroup.GroupId);

        if (group is null)
        {
            throw new Exception($"Group {selectedGroup.GroupName} not found!");
        }

        group.Name = groupNewName;
        _context.SaveChanges();
    }

    public void UpdateTeacher(GroupDto selectedGroup, TeacherDto selectedTeacher)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Id == selectedGroup.GroupId);

        if (group is null)
        {
            throw new Exception($"Group '{selectedGroup.GroupName}' not found!");
        }

        var teacherFullName = $"{selectedTeacher.Name} {selectedTeacher.Surname}";
        var teacher = _context.Teachers.FirstOrDefault(t => t.Id == selectedTeacher.TeacherId);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        group.TeacherId = teacher.Id;
        _context.SaveChanges();
    }
}
