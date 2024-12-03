using DAL.Entities;

namespace DAL.ORM.Repository;

public interface IGradeRepository : IRepository<Grade>
{
    Grade? GetByName(string name);
}