using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Data.Context;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T>(PostgresDbContextFactory dbContextFactory) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PostgresDbContextFactory _dbContextFactory = dbContextFactory;

        public async Task<Result<Guid, Error>> Insert(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatedAt = DateTime.UtcNow;
                context.Set<T>();
                var entry = context.Add(entity);
                await context.SaveChangesAsync();
                return entry.Entity.Id;
            }
            catch (Exception)
            {
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<Result<List<T>?, Error>> InsertRange(List<T> entities)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                entities.ForEach(x =>
                {
                    if (x.Id == Guid.Empty)
                    {
                        x.Id = Guid.NewGuid();
                    }

                    x.CreatedAt = DateTime.UtcNow;
                });

                context.Set<T>();
                context.AddRange(entities);
                await context.SaveChangesAsync();

                return new();
            }
            catch (Exception)
            {
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<Result<List<T>, Error>> GetAll()
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var entities = await context.Set<T>().ToListAsync();
                return entities;
            }
            catch (Exception)
            {
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<Result<T, Error>> GetById(Guid id)
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

        public async Task<Result<T, Error>> Update(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var result = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (result == null || result == default)
                {
                    throw new Exception("Entity not found");
                }

                entity.UpdatedAt = DateTime.UtcNow;

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

        public async Task<Result<bool, Error>> Delete(T entity)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                context.Set<T>();
                var removedEntity = context.Remove(entity);
                if (removedEntity == null || removedEntity == default)
                {
                    return false;
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<Result<bool, Error>> DeleteById(Guid id)
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
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }

        public async Task<Result<bool, Error>> DeleteRange(List<T> entities)
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
                return Error.InternalServerError("Algo deu errado! Contate a equipe de desenvolvimento.");
            }
            finally
            {
                context.Dispose();
            }
        }


    }
}