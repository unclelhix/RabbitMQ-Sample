using HashGenerator.Models;
using HashGenerator.Shared.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace HashGenerator.Core.DatabaseContext
{
    public interface IHashGeneratorDbContext : IDbContext
    {
        DbSet<Hashes> Hashes { get; set; }
    }
}
