namespace ORM;

public class Empty : INaming
{
    public string Name { get; set; }

    public Empty(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}