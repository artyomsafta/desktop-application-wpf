using CsvHelper;
using System.Globalization;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.FileServices;

public class ImportFileService
{
    private WpfAppDbContext _context;
    private string _filePath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public ImportFileService(GroupDto selectedGroup, string path)
    {
        _context = new WpfAppDbContext();
        _filePath = path;
        _selectedGroupName = selectedGroup.GroupName;
        _studentsList = this.GetStudentsList();
    }

    public void ImportStudents()
    {
        if (_studentsList is null || _studentsList.Count is 0)
        {
            throw new Exception("Error! File has no data or wrong group selected for import.");
        }

        var groupId = _context.Groups
            .Where(g => g.Name == _selectedGroupName)
            .Select(g => g.Id)
            .FirstOrDefault();

        if (groupId == Guid.Empty)
        {
            throw new Exception($"Group {_selectedGroupName} not found!");
        }

        var studentsToDelete = _context.Students
            .Where(g => g.GroupId == groupId)
            .ToList();

        _context.Students.RemoveRange(studentsToDelete);
        _context.SaveChanges();

        var studentsToAdd = new List<Student>();

        foreach (var student in _studentsList)
        {
            studentsToAdd.Add(new Student
            {
                Id = Guid.NewGuid(),
                Name = student.Name,
                Surname = student.Surname,
                GroupId = groupId
            });
        }

        _context.Students.AddRange(studentsToAdd);
        _context.SaveChanges();
    }

    private List<StudentDto> GetStudentsList()
    {
        var parsedList = this.ParseStudents();
        return parsedList
            .Where(g => g.GroupName == _selectedGroupName)
            .ToList();
    }

    private List<StudentDto> ParseStudents()
    {
        var students = new List<StudentDto>();

        using (var reader = new StreamReader(_filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = csv.GetRecord<StudentDto>();
                students.Add(record);
            }
        }

        return students;
    }
}
