using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFPersonRepository : EFCoreRepository<Person>,
    IPersonRepository
{
    public EFPersonRepository(EFContext context) : base(context)
    {
    }
}