namespace Task8_WPF.DAL.Entities;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
}
