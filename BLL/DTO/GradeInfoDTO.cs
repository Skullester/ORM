namespace BLL.DTO;

public class GradeInfoDTO : IElementDTO
{
    public int Id { get; set; }

    public string? Name => Grade?.Name;
    public int? GradeId { get; set; }
    public GradeDTO? Grade { get; set; }
    public int StudentId { get; set; }

    public int DisciplineId { get; set; }

    public GradeInfoDTO(int studentId, int disciplineId, int? gradeId)
    {
        StudentId = studentId;
        DisciplineId = disciplineId;
        GradeId = gradeId;
    }
}