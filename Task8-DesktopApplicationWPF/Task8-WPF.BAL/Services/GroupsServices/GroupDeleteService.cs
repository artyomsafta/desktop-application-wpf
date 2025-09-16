using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupDeleteService
{
    private WpfAppDbContext _context = new WpfAppDbContext();
    private GroupDto _selectedGroup;

    public GroupDeleteService(GroupDto selectedGroup)
    {
        _selectedGroup = selectedGroup;
    }

    public void DeleteGroup()
    {
        var group = _context.Groups
            .Include(s => s.Students)
            .FirstOrDefault(g => g.Name == _selectedGroup.GroupName);

        if (group is null)
        {
            throw new Exception($"Group {_selectedGroup.GroupName} not found!");
        }

        if (group.Students.Any())
        {
            throw new Exception($"Group {_selectedGroup.GroupName} cannot be deleted because it has students in it!");
        }

        _context.Groups.Remove(group);
        _context.SaveChanges();
    }
}
