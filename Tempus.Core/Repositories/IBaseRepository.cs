namespace Tempus.Core.Repositories;

public interface IBaseRepository<T>
    where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<Guid> Delete(Guid id);
}