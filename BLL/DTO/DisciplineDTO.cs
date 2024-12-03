namespace BLL.DTO;

public class DisciplineDTO : IElementDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DisciplineDTO(string name)
    {
        Name = name;
    }
}