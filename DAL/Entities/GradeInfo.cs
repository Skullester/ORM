using System.ComponentModel.DataAnnotations;

// ReSharper disable CollectionNeverQueried.Global

namespace DAL.Entities;

public class GradeInfo
{
    public int Id { get; set; }

    public string? Name => Grade?.Name;
    public int? GradeId { get; set; }
    public Grade? Grade { get; set; }
    public int StudentId { get; set; }

    public int DisciplineId { get; set; }

    [Timestamp] public byte[]? Timestamp { get; set; }

    public GradeInfo(int studentId, int disciplineId, int? gradeId)
    {
        StudentId = studentId;
        DisciplineId = disciplineId;
        GradeId = gradeId;
    }

    public override string ToString() => Name ?? "null";
}