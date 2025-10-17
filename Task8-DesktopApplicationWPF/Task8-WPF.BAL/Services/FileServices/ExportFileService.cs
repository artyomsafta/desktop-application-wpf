using CsvHelper;
using System.Globalization;
using Task8_WPF.BAL.Dto.EntityDtos;

namespace Task8_WPF.BAL.Services.FileServices;

public class ExportFileService
{
    public void ExportStudents(GroupDto selectedGroup, string path)
    {
        var studentsList = new DtoListsService().GetStudentsList(selectedGroup);

        if (studentsList is null || studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        using var writer = new StreamWriter(path, append: false);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(studentsList);
    }
}
