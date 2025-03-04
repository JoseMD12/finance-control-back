using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace Domain.Interface.Repositories
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        public Task<Result<Guid, Error>> Insert(T entity);
        public Task<Result<List<T>?, Error>> InsertRange(List<T> entities);
        public Task<Result<List<T>, Error>> GetAll();
        public Task<Result<T, Error>> GetById(Guid id);
        public Task<Result<T, Error>> Update(T entity);
        public Task<Result<bool, Error>> Delete(T entity);
        public Task<Result<bool, Error>> DeleteById(Guid id);
        public Task<Result<bool, Error>> DeleteRange(List<T> entities);
    }
}
