namespace Task8_WPF.DAL.Entities;

public class Teacher
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public List<Group> Groups { get; set; } = new();
}
