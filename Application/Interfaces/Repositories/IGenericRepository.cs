namespace Application.Interfaces.Repositories;

public interface IGenericRepository<T>
{
    Task<T>Save(T entity);

    Task<IEnumerable<T>> FindAll();

    Task Truncate(string table);
}