using System.Linq;
using System.Threading.Tasks;
using Authentication.Application.Users.DTOs;
using Authentication.Domain.Entities;
using Authentication.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    public class UsersController : BaseController
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = Context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            var user = await Context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var userEntity = new User() { Name = user.Name };
            Context.Users.Add(userEntity);
            await Context.SaveChangesAsync();

            return StatusCode(201, userEntity);
        }
    }
}