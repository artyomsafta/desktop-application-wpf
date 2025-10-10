using System.Reflection;
using Task8_WPF.BAL.Dto.EntityDtos;

namespace Task8_WPF.BAL.Services.FileServices;

public class ExportFileService
{
    public void ExportStudents(GroupDto selectedGroup, string path)
    {
        var studentsList = new DtoListsService().GetStudentsList(selectedGroup.GroupName);

        if (studentsList is null || studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        var fileHeader = string.Join(",",
            typeof(StudentDto)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name));

        using (var writer = new StreamWriter(path, append: false))
        {
            writer.WriteLine(fileHeader);
            foreach (var student in studentsList)
            { 
                writer.WriteLine($"{student.Name},{student.Surname},{student.GroupName}");
            }
        }
    }
}
