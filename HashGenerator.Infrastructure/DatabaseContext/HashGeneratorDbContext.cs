
using HashGenerator.Core.DatabaseContext;
using HashGenerator.Models;
using HashGenerator.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HashGenerator.Infrastructure.DatabaseContext
{
    public class HashGeneratorDbContext : DbContext, IHashGeneratorDbContext
    {       
        public HashGeneratorDbContext(DbContextOptions<HashGeneratorDbContext> options) :base(options){}      
        public DbSet<Hashes> Hashes { get; set; }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity &&
             (e.State == EntityState.Added || e.State == EntityState.Modified)).Select(x => x.Entity as BaseEntity);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
