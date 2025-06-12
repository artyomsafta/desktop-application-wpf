namespace Task8_WPF.BAL.Dto;

public class CoursesTreeDto
{
    public string CourseName { get; set; }
    public List<GroupsTreeDto> Groups { get; set; } = new();
}
