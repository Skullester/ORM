namespace ORM;

public class GradeStudentDiscipline
{
    public int Id { get; set; }

    public int GradeId { get; set; }

    public int StudentId { get; set; }

    public int DisciplineId { get; set; }

    public GradeStudentDiscipline(int studentId, int disciplineId, int gradeId)
    {
        StudentId = studentId;
        DisciplineId = disciplineId;
        GradeId = gradeId;
    }
}