namespace ORM;

public class Discipline : INaming
{
    public int Id { get; set; }

    public string Name { get; set; }

    private IEnumerable<Semester> Semesters { get; set; } = null!;

    public Discipline(string name)
    {
        Name = name;
    }
}