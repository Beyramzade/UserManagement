using Usermanagement.Domain.User;

namespace Usermanagement.Infrastructure.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddActivationCode(ActivationCode model);
        Task<bool> UpdateActivationCodes(string email);
    }
}
