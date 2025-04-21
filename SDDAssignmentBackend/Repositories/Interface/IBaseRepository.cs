using SDDAssignmentBackend.Entities;

namespace SDDAssignmentBackend.Repositories.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);
        T Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        IQueryable<T> AsQuerable();
    }
}
