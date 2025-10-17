namespace Task8_WPF.BAL.Dto.TreeDtos;

public class CoursesTreeDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public List<GroupsTreeDto> Groups { get; set; } = new();
}
