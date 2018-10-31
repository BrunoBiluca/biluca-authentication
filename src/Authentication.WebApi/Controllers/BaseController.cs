using Authentication.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        private AuthenticationDbContext context;

        protected AuthenticationDbContext Context => context ?? (context = HttpContext.RequestServices.GetService<AuthenticationDbContext>());
    }
}