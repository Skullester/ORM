namespace ORM;

public class Grade : INaming
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Grade(string name)
    {
        Name = name;
    }
}