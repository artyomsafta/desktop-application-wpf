namespace Task8_WPF.BAL.Dto;

public class CoursesTreeDto
{
    public string Title { get; set; }
    public List<CoursesTreeDto> Children { get; set; }

    public bool HasChildren => Children?.Any() == true;
}
