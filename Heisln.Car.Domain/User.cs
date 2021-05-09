using System;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Heisln.Car.Domain
{
    public class User
    {
        [BsonId]
        public Guid Id;

        public string Email;

        public string Password { get; private set; }

        public string FirstName;

        public string LastName { get; set; }

        public DateTime Birthday;

        public User(Guid id, string email, string password, string firstName, string lastName, DateTime birthday)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public bool CheckPassword(string password)
        {
            return password == Password;
        }

        public static User Create(string email, string password, string firstName, string lastName, DateTime birthday)
        {
            Guid newId = Guid.NewGuid();
            return new User(newId, email, password, firstName, lastName, birthday);
        }
    }
}
