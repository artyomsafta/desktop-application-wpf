using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services;

public class TreeViewService
{
    private WpfAppDbContext _context;

    public TreeViewService()
    {
        _context = new WpfAppDbContext();
    }

    public List<CoursesTreeDto> GetHierarchyForTreeView()
    {
        var hierarchyFromDb = _context.Courses
            .Include(c => c.Groups)
            .ThenInclude(g => g.Students)
            .AsNoTracking()
            .ToList();

        var hierarchyToDtos = hierarchyFromDb.Select(c => new CoursesTreeDto
        {
            CourseName = c.Name,
            Groups = c.Groups.Select(g => new GroupsTreeDto
            {
                GroupName = g.Name,
                Students = g.Students.Select(s => new StudentsTreeDto
                {
                    FullName = $"{s.Name} {s.Surname}"
                }).ToList()
            }).ToList()
        }).ToList();

        return hierarchyToDtos;
    }
}
