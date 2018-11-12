using System.Linq;
using System.Threading.Tasks;
using Authentication.Application.Users.Commands.CreateUser;
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
            .Select(user => UserBean.Projection(user));
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await Context.Users.FindAsync(id);

            var userResponse = UserBean.Projection(user);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand request)
        {
            return StatusCode(201, await Mediator.Send(request));
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

            var userResponse = UserBean.Projection(userDb);
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