using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using Usermanagement.Domain.User;
using Usermanagement.Infrastructure.Interfaces;

namespace Usermanagement.Infrastructure.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMongoCollection<ActivationCode> _collection;

        public IdentityRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<ActivationCode>(collectionName);
        }

        public async Task AddActivationCode(ActivationCode model)
        {
            await _collection.InsertOneAsync(model);
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateActivationCodes(string email)
        {
            var filter = Builders<ActivationCode>.Filter.Eq("email", email);

            // Define the update operation
            var update = Builders<ActivationCode>.Update
                .Set("IsActive", false);// Update the "Age" field to 30
            // Execute the update operation
            var updateResult = await _collection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}
