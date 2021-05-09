using Heisln.Car.Domain;
using MongoDB.Driver;

namespace Heisln.Car.Infrastructure
{
    public interface IMongoUserDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}