using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Task8_WPF.BAL.Dto.EntityDtos;
using Task8_WPF.BAL.Services.DtoListsServices;

namespace Task8_WPF.BAL.Services.FileServices;

public class CreatePdfFileService : IDocument
{
    private string _fullPath;
    private string _selectedGroupName;
    private List<StudentDto> _studentsList;

    public CreatePdfFileService(GroupDto selectedGroup, string folderPath)
    {
        _selectedGroupName = selectedGroup.GroupName;
        _fullPath = Path.Combine(folderPath, $"{selectedGroup.CourseName}_{selectedGroup.GroupName}.pdf");
        _studentsList = new StudentsListService().GetStudentsList(_selectedGroupName);
    }

    public void ExportStudentsToPdf()
    {
        this.GeneratePdf(_fullPath);
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        if (_studentsList is null || _studentsList.Count is 0)
        {
            throw new Exception("Error! Group has no students or wrong group selected for export.");
        }

        container.Page(page => 
        {
            page.Margin(30);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.White);

            page.Header()
            .Text($"{_selectedGroupName} students list")
            .FontSize(20)
            .Bold()
            .AlignCenter();

            page.Content().Table(table => 
            {
                table.ColumnsDefinition(columns => 
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header => 
                {
                    header.Cell().Element(CellStyle).Text("Number").Bold();
                    header.Cell().Element(CellStyle).Text("Name").Bold();
                    header.Cell().Element(CellStyle).Text("Surname").Bold();
                });

                for (int i = 0; i < _studentsList.Count; i++)
                {
                    table.Cell().Element(CellStyle).Text((i + 1).ToString());
                    table.Cell().Element(CellStyle).Text(_studentsList[i].Name);
                    table.Cell().Element(CellStyle).Text(_studentsList[i].Surname);
                }
            });
        });
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container
                .PaddingVertical(5)
                .PaddingHorizontal(10)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2);
    }
}
