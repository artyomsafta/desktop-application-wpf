namespace Task8_WPF.DAL.Entities;

public class Course
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<Group> Groups { get; set; } = new();
}
