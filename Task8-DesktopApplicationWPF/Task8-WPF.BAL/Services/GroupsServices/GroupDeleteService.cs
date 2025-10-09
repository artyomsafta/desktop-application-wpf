using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupDeleteService
{
    private WpfAppDbContext _context;

    public GroupDeleteService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupDeleteService()
    {
        _context = new WpfAppDbContext();
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
}
