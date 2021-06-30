using System.Transactions;
using Artist.Model;

namespace Artist.Interfaces
{
    public interface IDbContextFactory
    {
        ArtistDbContext Create();
        TransactionScope CreateReadUncommitedTransactionScope();
    }
}