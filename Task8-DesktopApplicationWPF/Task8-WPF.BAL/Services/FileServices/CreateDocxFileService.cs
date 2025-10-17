using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Task8_WPF.BAL.Dto.EntityDtos;

namespace Task8_WPF.BAL.Services.FileServices;

public class CreateDocxFileService
{
    public void ExportStudentsToDocx(GroupDto selectedGroup, string folderPath)
    {
        var fullPath = Path.Combine(folderPath, $"{selectedGroup.CourseName}_{selectedGroup.GroupName}.docx");
        var studentsList = new DtoListsService().GetStudentsList(selectedGroup);

        if (studentsList is null || studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        using (var docx = WordprocessingDocument.Create(fullPath, WordprocessingDocumentType.Document))
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

            for (int i = 0; i < studentsList.Count; i++)
            {
                TableRow row = new TableRow(
                    CreateCell((i + 1).ToString()),
                    CreateCell(" "),
                    CreateCell(studentsList[i].Name),
                    CreateCell(" "),
                    CreateCell(studentsList[i].Surname)
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
