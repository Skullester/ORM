namespace ORM;

public class Empty : IElement
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Empty(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}