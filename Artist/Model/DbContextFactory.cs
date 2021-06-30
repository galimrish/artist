using System.Data.Entity;
using System.Transactions;
using Artist.Interfaces;

namespace Artist.Model
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly int _connectionTimeout;

        public DbContextFactory(IAppSettings appSettings)
        {
            _connectionTimeout = appSettings.ConnectionTimeout;
        }

        public ArtistDbContext Create()
        {
            var context = new ArtistDbContext();
            Database.SetInitializer(new DbInitializer());
            context.Configuration.LazyLoadingEnabled = false;
            context.Database.CommandTimeout = _connectionTimeout;

            return context;
        }

        public TransactionScope CreateReadUncommitedTransactionScope()
        {
            var tranScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });

            return tranScope;
        }
    }
}
