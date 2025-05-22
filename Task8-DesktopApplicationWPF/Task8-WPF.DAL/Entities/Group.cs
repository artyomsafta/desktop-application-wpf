namespace Task8_WPF.DAL.Entities;

public class Group
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }

    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public List<Student> Students { get; set; } = new();
}
