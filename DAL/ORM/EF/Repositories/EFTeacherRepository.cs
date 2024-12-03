using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFTeacherRepository : EFCoreRepository<Teacher>,
    ITeacherRepository
{
    public EFTeacherRepository(EFContext context) : base(context)
    {
    }
}