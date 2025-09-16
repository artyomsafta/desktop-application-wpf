using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupUpdateTeacherService
{
    private WpfAppDbContext _context = new WpfAppDbContext();
    private string _selectedGroupName;
    private TeacherDto _teacherToUpdate;

    public GroupUpdateTeacherService(GroupDto selectedGroup, TeacherDto selectedTeacher)
    {
        _selectedGroupName = selectedGroup.GroupName;
        _teacherToUpdate = selectedTeacher;
    }

    public void UpdateTeacher()
    {
        var group = _context.Groups.FirstOrDefault(g => g.Name == _selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group '{_selectedGroupName}' not found!");
        }

        var teacherFullName = $"{_teacherToUpdate.Name} {_teacherToUpdate.Surname}";
        var teacher = _context.Teachers.FirstOrDefault(t => t.Name + " " + t.Surname == teacherFullName);

        if (teacher is null)
        {
            throw new Exception($"Teacher '{teacherFullName}' not found!");
        }

        group.TeacherId = teacher.Id;
        _context.SaveChanges();
    }
}
