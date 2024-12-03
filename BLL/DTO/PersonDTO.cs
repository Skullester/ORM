namespace BLL.DTO;

public class PersonDTO : IElementDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    protected PersonDTO(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}