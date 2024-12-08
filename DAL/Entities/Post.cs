namespace DAL.Entities;

public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Post(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}