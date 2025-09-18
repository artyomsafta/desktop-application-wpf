using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.FileServices;

public class ExportFileService
{
    private WpfAppDbContext _context;
    private string _filePath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public ExportFileService(GroupDto selectedGroup, string path)
    {
        _context = new WpfAppDbContext();
        _filePath = path;
        _selectedGroupName = selectedGroup.GroupName;
        _studentsList = this.GetStudentsList();
    }

    public void ExportStudents()
    {
        if (_studentsList is null || _studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        var fileHeader = string.Join(",",
            typeof(StudentDto)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name));

        using (var writer = new StreamWriter(_filePath, append: false))
        {
            writer.WriteLine(fileHeader);
            foreach (var student in _studentsList)
            { 
                writer.WriteLine($"{student.Name},{student.Surname},{student.GroupName}");
            }
        }
    }

    private List<StudentDto> GetStudentsList()
    {
        var groupId = _context.Groups
       .Where(g => g.Name == _selectedGroupName)
       .Select(g => g.Id)
       .FirstOrDefault();

        if (groupId == Guid.Empty)
        {
            throw new Exception($"Group {_selectedGroupName} not found!");
        }

        var students = _context.Students
            .Where(g => g.GroupId == groupId)
            .Include(g => g.Group)
            .ToList();

        var studentsList = new List<StudentDto>();

        foreach (var student in students)
        {
            studentsList.Add(new StudentDto
            {
                Name = student.Name,
                Surname = student.Surname,
                GroupName = student.Group.Name
            });
        }

        return studentsList;
    }
}
