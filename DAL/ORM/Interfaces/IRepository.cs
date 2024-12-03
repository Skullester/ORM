using System.Linq.Expressions;

namespace DAL.ORM.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity? GetBy(int id);
    ValueTask<TEntity?> GetByAsync(int id);
    void DeleteBy(int id);
    IQueryable<TEntity> GetAll();
    bool Contains(TEntity entity);
    IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    void Include<TProperty>(Expression<Func<TEntity, TProperty>> navigation);
}