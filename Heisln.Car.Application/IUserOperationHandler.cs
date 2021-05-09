using System;
using System.Threading.Tasks;

namespace Heisln.Car.Application
{
    public interface IUserOperationHandler
    {
        Task<(string, Guid)> Login(string username, string password);

        Task<(string, Guid)> Register(string email, string password, string firstName, string lastName, DateTime birthday);

        Task Edit(Guid id, string email, string firstName, string lastName, DateTime birthday);
    }
}
