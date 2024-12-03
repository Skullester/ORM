using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFGroupRepository : EFCoreRepository<Group>,
    IGroupRepository
{
    public EFGroupRepository(EFContext context) : base(context)
    {
    }
}