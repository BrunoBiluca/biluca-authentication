using System.Linq;
using System.Threading.Tasks;
using Authentication.Application.Users.DTOs;
using Authentication.Application.Users.Helpers;
using Authentication.Domain.Entities;
using Authentication.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.WebApi.Controllers
{
    // TODO: este controller irá redirecionar os comandos e queries que estão implementados no projeto application
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = Context.Users.ToList()
            .Select(user =>
            {
                return new UserDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                };
            });
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await Context.Users.FindAsync(id);

            var userResponse = new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
            };
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var salt = UserUtils.GetSalt();

            var encryptedPassword = UserUtils.GenerateSecurePassword(user.Password, salt);

            var userEntity = new User()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Salt = salt,
                EncryptedPassword = encryptedPassword
            };
            Context.Users.Add(userEntity);
            await Context.SaveChangesAsync();

            var userResponse = new UserDTO()
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
            };
            return StatusCode(201, userResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO user){
            
            //Validations
            if(user == null || user.Id != id)
                return BadRequest();

            var userDb = await Context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if(userDb == null) return NotFound();

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            await Context.SaveChangesAsync();

            var userResponse = new UserDTO()
            {
                Id = userDb.Id,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                UserName = userDb.UserName,
                Email = userDb.Email,
            };
            return Ok(userResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){

            var userDb = await Context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if(userDb == null) return NotFound();

            Context.Users.Remove(userDb);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}