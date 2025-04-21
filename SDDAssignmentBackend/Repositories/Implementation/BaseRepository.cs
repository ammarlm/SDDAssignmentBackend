using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Repositories.Interface;

namespace SDDAssignmentBackend.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public T? GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(m => m.Id == id);
        }
        public Task<T?> GetByIdAsync(Guid id)
        {
            return _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public IQueryable<T> AsQuerable()
        {
            return _dbSet.AsQueryable();
        }

        public Task ExecuteSqlRawAsync(string query, params object[] parameter)
        {
            return _context.Database.ExecuteSqlRawAsync(query, parameter);
        }
    }
}
