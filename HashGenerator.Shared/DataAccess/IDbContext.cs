using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HashGenerator.Shared.DataAccess
{
    public interface IDbContext
    {
        /// <summary>
        /// Creates the transaction for the Command for updates or insert of entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Saves the changes from entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Sets an Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
