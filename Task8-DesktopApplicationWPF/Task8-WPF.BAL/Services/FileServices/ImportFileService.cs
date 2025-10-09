using CsvHelper;
using System.Globalization;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;
using Task8_WPF.DAL.Entities;

namespace Task8_WPF.BAL.Services.FileServices;

public class ImportFileService
{
    private WpfAppDbContext _context;

    public ImportFileService()
    {
        _context = new WpfAppDbContext();
    }

    public void ImportStudents(GroupDto selectedGroup, string path)
    {
        var selectedGroupName = selectedGroup.GroupName;
        var studentsList = this.ParseStudents(path, selectedGroupName);

        if (studentsList is null || studentsList.Count is 0)
        {
            throw new Exception("Error! File has no data or wrong group selected for import.");
        }

        var groupId = _context.Groups
            .Where(g => g.Name == selectedGroupName)
            .Select(g => g.Id)
            .FirstOrDefault();

        if (groupId == Guid.Empty)
        {
            throw new Exception($"Group {selectedGroupName} not found!");
        }

        var studentsToDelete = _context.Students
            .Where(g => g.GroupId == groupId)
            .ToList();

        _context.Students.RemoveRange(studentsToDelete);
        _context.SaveChanges();

        var studentsToAdd = new List<Student>();

        foreach (var student in studentsList)
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

    private List<StudentDto> ParseStudents(string path, string selectedGroupName)
    {
        var students = new List<StudentDto>();

        using (var reader = new StreamReader(path))
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

        return students
            .Where(g => g.GroupName == selectedGroupName)
            .ToList();
    }
}
