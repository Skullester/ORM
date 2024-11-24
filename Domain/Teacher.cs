namespace ORM;

public class Teacher : Person
{
    public int PostId { get; set; }
    public Post? Post { get; set; }

    public Teacher(string name, int postId) : base(name)
    {
        PostId = postId;
    }

    public override string ToString() => $"{Post} {base.ToString()}";
}