using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFDisciplineRepository : EFCoreRepository<Discipline>, IDisciplineRepository
{
    public EFDisciplineRepository(EFContext context) : base(context)
    {
    }
}