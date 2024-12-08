namespace DAL.Entities;

public class Grade
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Grade(string name)
    {
        Name = name;
    }
}