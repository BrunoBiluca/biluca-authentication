using System;
using System.Threading.Tasks;
using Authentication.Application.Users.DTOs;
using Authentication.Application.Users.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.WebApi.Controllers
{
    [Route("api/authenticate")]
    public class AuthenticationController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Authenticate(UserDTO user)
        {
            if(string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return BadRequest("UserName or Password is empty");

            var userDb = await Context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if(userDb == null) return Unauthorized();

            var encryptedPassword = UserUtils.GenerateSecurePassword(user.Password, userDb.Salt);
            if(userDb.EncryptedPassword != encryptedPassword) {
                return Unauthorized();
            }

            // Update Token in User
            var salt = UserUtils.GetSalt();
            var accessTokenMaterial = userDb.Id + salt;

            var encryptedAccessToken = UserUtils.Encrypt(encryptedPassword, accessTokenMaterial);
            var accessTokenBase64Encode = Convert.ToBase64String(encryptedAccessToken);

            var tokenLenght = accessTokenBase64Encode.Length;

            var tokenToSaveInDatabase = accessTokenBase64Encode.Substring(0, tokenLenght / 2);
            var returnedToken = accessTokenBase64Encode.Substring(tokenLenght / 2, tokenLenght - tokenLenght / 2);

            userDb.Token = tokenToSaveInDatabase;
            await Context.SaveChangesAsync();

            // Response
            var userResponse = new UserDTO()
            {
                Id = userDb.Id,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                UserName = userDb.UserName,
                Email = userDb.Email,
                Token = returnedToken
            };
            return Ok(userResponse);
        }
    }
}