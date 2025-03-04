using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Dtos;
using Domain.Entities.Sheet;
using Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Utils.Tools;

namespace Data.Repositories
{
    public class SheetTemplateRepository(PostgresDbContextFactory dbContextFactory)
        : BaseRepository<SheetTemplateEntity>(dbContextFactory),
            ISheetTemplateRepository
    {
        public new async Task<Result<List<SheetTemplateEntity>, Error>> GetAll()
        {
            Console.WriteLine("entrou aqui");
            using var context = _dbContextFactory.CreateDbContext([]);
            Console.WriteLine("criou contexto");
            try
            {
                var entities = await context
                    .Set<SheetTemplateEntity>()
                    .Include(x => x.Columns)
                    .ToListAsync();

                JsonTool.Print("entities", entities);

                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Error.InternalServerError(
                    "Algo deu errado! Contate a equipe de desenvolvimento."
                );
            }
            finally
            {
                context.Dispose();
            }
        }

        public new async Task<Result<SheetTemplateEntity, Error>> GetById(Guid id)
        {
            using var context = _dbContextFactory.CreateDbContext([]);
            try
            {
                var entity = await context
                    .Set<SheetTemplateEntity>()
                    .Include(x => x.Columns)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                {
                    return Error.NotFound("Entidade nao encontrada!");
                }

                return entity;
            }
            catch (Exception)
            {
                return Error.NotFound("Entidade não encontrada!");
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}
