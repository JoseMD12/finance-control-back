
namespace Application.Interfaces.Auth
{
    public interface IDecodeBasicAuth
    {
        public (string Email, string Password) Execute(string authHeader);
    }
}