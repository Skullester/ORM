namespace BLL.DTO;

public class StudentDTO : PersonDTO
{
    public int GroupId { get; set; }
    public GroupDTO? Group { get; set; }

    public StudentDTO(string name, int groupId) : base(name)
    {
        GroupId = groupId;
    }
}