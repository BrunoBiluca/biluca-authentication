using Authentication.Application.Users.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Application.Users.Helpers
{
    public static class UserBean
    {
        public static UserDTO Projection(User user){
            return new UserDTO(){
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}