using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFSemesterRepository : EFCoreRepository<Semester>,
    ISemesterRepository
{
    public EFSemesterRepository(EFContext context) : base(context)
    {
    }
}