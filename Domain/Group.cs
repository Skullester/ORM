namespace ORM;

public class Group : INaming
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Student> Students { get; set; } = null!;
    public int SemesterId { get; set; }
    public Semester? Semester { get; set; }

    public Group(string name, int semesterId)
    {
        Name = name;
        SemesterId = semesterId;
    }

    public override string ToString() => $"Группа {Name} Семестр: {Semester?.Name ?? "?"}:";
}