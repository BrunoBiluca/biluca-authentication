using Authentication.Application.Users.DTOs;
using MediatR;

namespace Authentication.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}