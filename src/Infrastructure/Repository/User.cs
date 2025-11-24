

using Domain.Error;
using Infrastructure.Persistence;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class User(PostgresContext context) : Base<Domain.Entities.User>(context), IUser
    {
        public Task<Domain.Entities.User?> GetByEmailAsync(string email)
        {
            return _dbSet.FirstOrDefaultAsync(u => u.Email == email) ?? throw Error.ParseError($"User not found", 404);
        }
    }
}