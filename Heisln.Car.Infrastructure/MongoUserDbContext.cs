using Heisln.Car.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Heisln.Car.Infrastructure
{
    public class MongoUserDbContext : IMongoUserDbContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        private MongoCollectionSettings _collectionSettings { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoUserDbContext(IOptions<Mongosettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
            _collectionSettings = new MongoCollectionSettings
            {
                GuidRepresentation = GuidRepresentation.Standard
            };
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>(nameof(User),_collectionSettings);
    }
}