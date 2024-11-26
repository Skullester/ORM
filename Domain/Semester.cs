namespace ORM;

public class Semester : IElement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Discipline> Disciplines { get; set; } = null!;
    public IEnumerable<Group> Groups { get; set; } = null!;

    public Semester(string name)
    {
        Name = name;
    }
}