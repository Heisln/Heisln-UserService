using Heisln.Api.Models;
using Heisln.Car.Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Heisln.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserOperationHandler userOperationHandler;

        public UserController(IUserOperationHandler userOperationHandler)
        {
            this.userOperationHandler = userOperationHandler;
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponse> UserLogin(AuthenticationRequest request)
        {
            var result = await userOperationHandler.Login(request.Email, request.Password);
            return new AuthenticationResponse { Token = result.Item1, UserId = result.Item2 };
        }

        [HttpPost("registration")]
        public async Task<AuthenticationResponse> RegistrateUser([FromBody] User newUser)
        {
            var result = await userOperationHandler.Register(newUser.Email, newUser.Password, newUser.FirstName, newUser.LastName, newUser.Birthday);
            return new AuthenticationResponse() { Token = result.Item1, UserId = result.Item2} ;
        }

        [HttpPost("edit")]
        public async Task EditUser([FromBody] EditUser editUser)
        {
            await userOperationHandler.Edit(editUser.Id, editUser.Email, editUser.FirstName, editUser.LastName, editUser.Birthday);
        }
    }
}
