using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;

namespace Task8_WPF.BAL.Services.FileServices;

public class CreateDocxFileService
{
    private string _fullPath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public CreateDocxFileService(GroupDto selectedGroup, string folderPath)
    {
        _selectedGroupName = selectedGroup.GroupName;
        _fullPath = Path.Combine(folderPath, $"{selectedGroup.CourseName}_{selectedGroup.GroupName}.docx");
        _studentsList = new StudentsListService().GetStudentsList(_selectedGroupName);
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
}
