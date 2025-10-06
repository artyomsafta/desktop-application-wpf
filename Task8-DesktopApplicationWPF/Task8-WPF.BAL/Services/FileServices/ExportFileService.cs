using System.Reflection;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;

namespace Task8_WPF.BAL.Services.FileServices;

public class ExportFileService
{
    private string _filePath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public ExportFileService(GroupDto selectedGroup, string path)
    {
        _filePath = path;
        _selectedGroupName = selectedGroup.GroupName;
        _studentsList = new StudentsListService().GetStudentsList(_selectedGroupName);
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
}
