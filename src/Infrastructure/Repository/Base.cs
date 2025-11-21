using Domain.Entities;
using Domain.Error;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class Base<T> : IBase<T> where T : TEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Base(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null) throw Error.ParseError($"{typeof(T).Name} not able to create", 400);

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            entity = (await _dbSet.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) ?? throw Error.ParseError($"{typeof(T).Name} not found", 404);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw Error.ParseError($"{typeof(T).Name} not found", 404);

            var result = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id) ?? throw Error.ParseError($"{typeof(T).Name} not found", 404);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

        }
    }
}