using DAL.Entities;

namespace DAL.ORM.Repository;

public class EfGradeInfoRepository : EFCoreRepository<GradeInfo>,
    IGradeInfoRepository
{
    public EfGradeInfoRepository(EFContext context) : base(context)
    {
    }
}