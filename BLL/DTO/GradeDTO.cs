namespace BLL.DTO;

public class GradeDTO : IElementDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public GradeDTO(string name)
    {
        Name = name;
    }
}