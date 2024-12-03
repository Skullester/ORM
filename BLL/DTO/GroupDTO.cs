namespace BLL.DTO;

public class GroupDTO : IElementDTO
{
    public int Id { get; set; }
    private readonly string name;
    public string Name => $"Группа {name} Семестр {Semester?.Name ?? "?"}:";
    public int SemesterId { get; set; }
    public SemesterDTO? Semester { get; set; }

    public GroupDTO(string name, int semesterId)
    {
        this.name = name;
        SemesterId = semesterId;
    }
}