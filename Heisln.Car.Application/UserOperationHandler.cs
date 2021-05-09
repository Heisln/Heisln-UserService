using Heisln.Car.Contract;
using Heisln.Car.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisln.Car.Application
{
    public class UserOperationHandler : IUserOperationHandler
    {
        readonly IUserRepository userRepository;

        public UserOperationHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<(string, Guid)> Login(string email, string password)
        {
            var user = userRepository.Get(email, password);

            var token = JWTTokenGenerator.CreateToken(user);

            return (token, user.Id);
        }

        public async Task<(string, Guid)> Register(string email, string password, string firstName, string lastName, DateTime birthday)
        {
            if (userRepository.CheckIfUserAlreadyExists(email))
            {
                return ("",new Guid());
            }
            var newUser = User.Create(email, password, firstName, lastName, birthday);
            userRepository.Add(newUser);

            var user = userRepository.Get(email, password);
            var token = JWTTokenGenerator.CreateToken(user);
            return (token, user.Id);
        }

        public async Task Edit(Guid id, string email, string firstName, string lastName, DateTime birthday)
        {
            var user = userRepository.Get(id);
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Birthday = birthday;
            userRepository.Update(user);
        }
    }
}
