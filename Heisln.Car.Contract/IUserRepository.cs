using Heisln.Car.Domain;
using System;

namespace Heisln.Car.Contract
{
    public interface IUserRepository
    {
        public static string Secret = "DH7ND98DDSA13210FDSEKFJFJF543KJCKKOP543FKOPFLPÜF543KFKJKLRIFIORKL6894829"; //Move to settings-file
        User Get(string username, string password);
        User Get(Guid id);
        void Add(User entity);
        bool CheckIfUserAlreadyExists(string email);
        void Update(User user);
    }
}
