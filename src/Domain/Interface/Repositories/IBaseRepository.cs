using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<Guid> Insert(T entity);
        public Task InsertRange(List<T> entities);
        public Task<List<T>> GetAll();
        public Task<T?> GetById(Guid id);
        public Task<T> Update(T entity);
        public Task<bool> Delete(T entity);
        public Task<bool> DeleteById(Guid id);
        public Task<bool> DeleteRange(List<T> entities);
    }
}