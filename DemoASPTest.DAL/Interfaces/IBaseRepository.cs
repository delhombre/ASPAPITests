namespace DemoASPTest.DAL.Interfaces;

public interface IBaseRepository<TEntity, TId>
    where TEntity : class
{
    TId Create(TEntity entity);
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(TId id);
    bool Update(TId id, TEntity e);
    bool Delete(TId id);
}
