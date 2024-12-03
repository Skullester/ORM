using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL.ORM.Repository;

public abstract class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> dbSet;

    protected EFCoreRepository(EFContext context)
    {
        dbSet = context.Set<TEntity>();
    }

    public TEntity? GetBy(int id) => dbSet.Find(id);
    public ValueTask<TEntity?> GetByAsync(int id) => dbSet.FindAsync(id);

    public void DeleteBy(int id)
    {
        var entity = GetBy(id);
        if (entity is null)
            throw new ArgumentException("Объект с таким ключом не найден!");
        dbSet.Remove(entity);
    }

    public IQueryable<TEntity> GetAll() => dbSet;

    public bool Contains(TEntity entity) => dbSet.Contains(entity);

    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) => dbSet.Where(predicate);
    public void Add(TEntity entity) => dbSet.Add(entity);
    public void AddRange(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);
    public Task AddRangeAsync(IEnumerable<TEntity> entities) => dbSet.AddRangeAsync(entities);
    public void Remove(TEntity entity) => dbSet.Remove(entity);

    public void Update(TEntity entity) => dbSet.Update(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);
    public void Include<TProperty>(Expression<Func<TEntity, TProperty>> navigation) => dbSet.Include(navigation);
}