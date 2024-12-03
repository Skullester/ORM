using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFPostRepository : EFCoreRepository<Post>,
    IPostRepository
{
    public EFPostRepository(EFContext context) : base(context)
    {
    }
}