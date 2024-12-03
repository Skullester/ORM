using DAL.Entities;

namespace DAL.ORM.Repository;

public class EFGradeRepository : EFCoreRepository<Grade>, IGradeRepository
{
    public EFGradeRepository(EFContext context) : base(context)
    {
    }

    public Grade? GetByName(string name) =>
        GetAll().FirstOrDefault(x => x.Name.Equals(name));
}