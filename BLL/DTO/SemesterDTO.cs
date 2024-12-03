namespace BLL.DTO;

public class SemesterDTO : IElementDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SemesterDTO(string name)
    {
        Name = name;
    }
}