using CsvHelper.Configuration.Attributes;

namespace Task8_WPF.BAL.Dto.EntityDtos;

public class StudentDto
{
    [Ignore]
    public Guid StudentId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string GroupName { get; set; }
}
