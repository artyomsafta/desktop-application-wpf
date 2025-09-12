using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.GroupsServices;

public class GroupRenameService
{
    private WpfAppDbContext _context = new WpfAppDbContext();
    private string _groupNewName;
    private string _selectedGroupName;

    public GroupRenameService(string groupNewName, GroupDto selectedGroup)
    {
        _groupNewName = groupNewName;
        _selectedGroupName = selectedGroup.GroupName;
    }

    public void RenameGroup()
    {
        if (_context.Groups.Any(g => g.Name == _groupNewName))
        {
            throw new Exception("This group already exists! Try another name");
        }

        var group = _context.Groups.FirstOrDefault(g => g.Name == _selectedGroupName);

        if (group is null)
        {
            throw new Exception($"Group {_selectedGroupName} not found!");
        }

        group.Name = _groupNewName;
        _context.SaveChanges();
    }
}
