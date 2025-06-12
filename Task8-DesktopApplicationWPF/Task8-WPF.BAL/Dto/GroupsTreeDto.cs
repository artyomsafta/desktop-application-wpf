namespace Task8_WPF.BAL.Dto;

public class GroupsTreeDto
{
    public string GroupName { get; set; }
    public List<StudentsTreeDto> Students { get; set; } = new();
}
