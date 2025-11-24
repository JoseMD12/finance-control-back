namespace Infrastructure.Interfaces
{
    public interface IUser : IBase<Domain.Entities.User>
    {
        public Task<Domain.Entities.User?> GetByEmailAsync(string email);
    }
}