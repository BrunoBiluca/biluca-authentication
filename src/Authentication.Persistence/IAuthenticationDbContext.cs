using System.Threading;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence
{
    public interface IAuthenticationDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}