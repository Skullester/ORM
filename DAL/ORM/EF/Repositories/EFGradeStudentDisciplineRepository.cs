using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFGradeStudentDisciplineRepository : EFCoreRepository<GradeStudentDiscipline>,
    IGradeStudentDisciplineRepository
{
    public EFGradeStudentDisciplineRepository(EFContext context) : base(context)
    {
    }
}