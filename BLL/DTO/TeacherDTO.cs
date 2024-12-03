namespace BLL.DTO;

public class TeacherDTO : PersonDTO
{
    public int PostId { get; set; }
    public PostDTO? Post { get; set; }

    public TeacherDTO(string name, int postId) : base(name)
    {
        PostId = postId;
    }

    public override string ToString() => $"{Post} {base.ToString()}";
}