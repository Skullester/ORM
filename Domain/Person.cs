namespace ORM;

public class Person : INaming
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}