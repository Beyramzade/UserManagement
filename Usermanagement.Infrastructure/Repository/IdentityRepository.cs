using Usermanagement.Domain.User;
using Usermanagement.Infrastructure.Interfaces;

namespace Usermanagement.Infrastructure.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        public Task<bool> AddActivationCode(ActivationCode model)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateActivationCodes(string email)
        {
            throw new NotImplementedException();
        }
    }
}
