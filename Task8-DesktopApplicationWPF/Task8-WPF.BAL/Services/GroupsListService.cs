using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services;

public class GroupsListService
{
    private WpfAppDbContext _context;

    public GroupsListService(DbContextOptions<WpfAppDbContext> options)
    {
        _context = new WpfAppDbContext(options);
    }

    public GroupsListService()
    {
        _context = new WpfAppDbContext();
    }

    public List<GroupDto> GetGroupsList()
    {
        var gropusList = _context.Groups
            .Include(c => c.Course)
            .Include(t => t.Teacher)
            .ToList();

        return gropusList.Select(g => new GroupDto
        {
            GroupName = g.Name,
            CourseName = g.Course?.Name,
            TeacherFullName = $"{g.Teacher?.Name} {g.Teacher?.Surname}"
        }).ToList();
    }
}
