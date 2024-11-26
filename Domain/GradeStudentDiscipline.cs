using System.ComponentModel.DataAnnotations;

namespace ORM;

public class GradeStudentDiscipline : IElement
{
    public int Id { get; set; }

    public string? Name => Grade?.Name;
    public int? GradeId { get; set; }
    public Grade? Grade { get; set; }
    public int StudentId { get; set; }

    public int DisciplineId { get; set; }

    [Timestamp] public byte[]? Timestamp { get; set; }

    public GradeStudentDiscipline(int studentId, int disciplineId, int? gradeId)
    {
        StudentId = studentId;
        DisciplineId = disciplineId;
        GradeId = gradeId;
    }

    public override string ToString() => Name ?? "null";
}