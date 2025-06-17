namespace Task8_WPF.BAL.Dto.TreeDtos;

public class CoursesTreeDto
{
    public string CourseName { get; set; }
    public List<GroupsTreeDto> Groups { get; set; } = new();
}
