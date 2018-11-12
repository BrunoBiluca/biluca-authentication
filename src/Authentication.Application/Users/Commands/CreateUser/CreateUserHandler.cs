using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Users.DTOs;
using Authentication.Application.Users.Helpers;
using Authentication.Domain.Entities;
using Authentication.Persistence;
using MediatR;

namespace Authentication.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly AuthenticationDbContext context;

        public CreateUserHandler(AuthenticationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken){
            var salt = UserUtils.GetSalt();

            var encryptedPassword = UserUtils.GenerateSecurePassword(request.Password, salt);

            var userEntity = new User()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Salt = salt,
                EncryptedPassword = encryptedPassword
            };

            context.Users.Add(userEntity);
            await context.SaveChangesAsync(true, cancellationToken);

            return UserBean.Projection(userEntity);
        }
    }
}