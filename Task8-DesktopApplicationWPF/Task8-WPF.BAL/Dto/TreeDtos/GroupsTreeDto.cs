namespace Task8_WPF.BAL.Dto.TreeDtos;

public class GroupsTreeDto
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public List<StudentsTreeDto> Students { get; set; } = new();
}
