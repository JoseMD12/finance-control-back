using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBase<T> where T : TEntity
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(Guid id);
    }
}