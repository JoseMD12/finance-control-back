using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Entities;
using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T>(PostgresDbContextFactory dbContextFactory) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PostgresDbContextFactory _dbContextFactory = dbContextFactory;

        public async Task<Guid> Insert(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>();
                var entry = context.Add(entity);
                await context.SaveChangesAsync();
                return entry.Entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task InsertRange(List<T> entities)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>();
                context.AddRange(entities);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<List<T>> GetAll()
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var entities = await context.Set<T>().ToListAsync();
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<T?> GetById(Guid id)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<T> Update(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>();
                var entry = context.Update(entity);
                await context.SaveChangesAsync();

                return entry.Entity;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<bool> Delete(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>();
                context.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<bool> DeleteById(Guid id)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null || entity == default)
                {
                    return false;
                }
                context.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<bool> DeleteRange(List<T> entities)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>().RemoveRange(entities);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                context.Dispose();
            }
        }


    }
}