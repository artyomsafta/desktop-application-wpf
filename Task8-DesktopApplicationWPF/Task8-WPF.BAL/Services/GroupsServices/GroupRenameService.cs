using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupRenameService
{
    private WpfAppDbContext _context;

    public GroupRenameService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupRenameService()
    {
        _context = new WpfAppDbContext();
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
}
