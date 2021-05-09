using Heisln.Car.Contract;
using Heisln.Car.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Heisln.Car.Infrastructure
{
    public class UserRepository : EntityFrameWorkRepository, IUserRepository
    {
        public UserRepository(IMongoUserDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(User entity)
        {
            dbContext.Users.InsertOne(entity);
        }

        public User Get(string email, string password)
        {
            var user = dbContext.Users.Find(u => u.Email == email && u.Password == password).First();
            return user;
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return dbContext.Users.Find(u => u.Email == email).Any();
        }

        public User Get(Guid id)
        {
            var user = dbContext.Users.Find(u => u.Id == id).First();
            return user;
        }

        public void Update(User user)
        {
            var update = Builders<User>.Update
                                        .Set(nameof(user.Birthday), user.Birthday)
                                        .Set(nameof(user.FirstName), user.FirstName)
                                        .Set(nameof(user.LastName), user.LastName)
                                        .Set(nameof(user.Email), user.Email);
            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            dbContext.Users.UpdateOne(filter, update);
        }
    }
}
