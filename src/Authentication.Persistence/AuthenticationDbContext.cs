using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence
{
    public class AuthenticationDbContext : DbContext, IAuthenticationDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        public AuthenticationDbContext() { }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.Entries<IEntityDate>()
                .Where(E => E.State == EntityState.Added)
                .ToList()
                .ForEach(E =>
                {
                    E.Property(p => p.Created).CurrentValue = DateTime.Now;
                });

            ChangeTracker.Entries<IEntityDate>()
                .Where(E => E.State == EntityState.Modified)
                .ToList()
                .ForEach(E =>
                {
                    E.Property(p => p.Updated).CurrentValue = DateTime.Now;
                });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<User> Users { get; set; }
    }
}