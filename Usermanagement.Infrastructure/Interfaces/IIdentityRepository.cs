namespace Usermanagement.Infrastructure.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> AddActivationCode(string email, string code);
        Task<bool> UpdateActivationCodes(string email);
    }
}
