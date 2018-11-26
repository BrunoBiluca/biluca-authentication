using System.Threading;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using Authentication.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Application.Tests.Infrastructure
{
    public class TestDbContext : DbContext, IAuthenticationDbContext
    {
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}