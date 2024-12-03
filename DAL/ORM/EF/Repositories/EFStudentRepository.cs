using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFStudentRepository : EFCoreRepository<Student>,
    IStudentRepository
{
    public EFStudentRepository(EFContext context) : base(context)
    {
    }
}