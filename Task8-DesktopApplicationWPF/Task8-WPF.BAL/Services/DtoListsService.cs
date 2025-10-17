using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services;

public class DtoListsService
{
    private WpfAppDbContext _context;

    public DtoListsService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public DtoListsService()
    {
        _context = new WpfAppDbContext();
    }

    public List<CourseDto> GetCoursesList()
    {
        var coursesList = _context.Courses
            .ToList();

        return coursesList.Select(c => new CourseDto
        {
            CourseId = c.Id,
            CourseName = c.Name,
            Description = c.Description
        }).ToList();
    }

    public List<GroupDto> GetGroupsList()
    {
        var gropusList = _context.Groups
            .Include(c => c.Course)
            .Include(t => t.Teacher)
            .ToList();

        return gropusList.Select(g => new GroupDto
        {
            GroupId = g.Id,
            GroupName = g.Name,
            CourseName = g.Course.Name,
            TeacherFullName = $"{g.Teacher?.Name} {g.Teacher?.Surname}"
        }).ToList();
    }

    public List<StudentDto> GetStudentsList(GroupDto selectedGroup)
    {
        var students = _context.Students
            .Where(g => g.GroupId == selectedGroup.GroupId)
            .Include(g => g.Group)
            .ToList();

        var studentsList = new List<StudentDto>();

        foreach (var student in students)
        {
            studentsList.Add(new StudentDto
            {
                StudentId = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                GroupName = student.Group.Name
            });
        }

        return studentsList;
    }

    public List<TeacherDto> GetTeachersList()
    {
        var teachersList = _context.Teachers
            .ToList();

        return teachersList.Select(t => new TeacherDto
        {
            TeacherId = t.Id,
            Name = t.Name,
            Surname = t.Surname,
        }).ToList();
    }
}
