namespace BLL.DTO;

public class Empty : IElementDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Empty(string name)
    {
        Name = name;
    }
}