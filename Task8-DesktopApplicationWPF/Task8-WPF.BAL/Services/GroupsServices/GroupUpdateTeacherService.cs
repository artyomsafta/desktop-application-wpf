using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupUpdateTeacherService
{
    private WpfAppDbContext _context;

    public GroupUpdateTeacherService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupUpdateTeacherService()
    {
        _context = new WpfAppDbContext();
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
