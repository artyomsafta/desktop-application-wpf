using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.DAL;

namespace Task8_WPF.BAL.Services.FileServices;

public class CreateDocxFileService
{
    private WpfAppDbContext _context;
    private string _fullPath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public CreateDocxFileService(GroupDto selectedGroup, string folderPath)
    {
        _context = new WpfAppDbContext();
        _selectedGroupName = selectedGroup.GroupName;
        _fullPath = $"{folderPath}\\{selectedGroup.CourseName}_{_selectedGroupName}.docx";
        _studentsList = this.GetStudentsList();
    }

    public void ExportStudentsToDocx()
    {
        if (_studentsList is null || _studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        using (var docx = WordprocessingDocument.Create(_fullPath, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = docx.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());

            var table = new Table();

            TableRow headerRow = new TableRow(
                CreateCell("Number"),
                CreateCell(" "),
                CreateCell("Name"),
                CreateCell(" "),
                CreateCell("Surname")
            );
            table.Append(headerRow);

            for (int i = 0; i < _studentsList.Count; i++)
            {
                TableRow row = new TableRow(
                    CreateCell((i + 1).ToString()),
                    CreateCell(" "),
                    CreateCell(_studentsList[i].Name),
                    CreateCell(" "),
                    CreateCell(_studentsList[i].Surname)
                );
                table.Append(row);
            }

            mainPart.Document.Body.Append(table);
            mainPart.Document.Save();
        }
    }

    private TableCell CreateCell(string text)
    {
        return new TableCell(
            new Paragraph(
                new Run(
                    new Text(text)
                )
            )
        );
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
