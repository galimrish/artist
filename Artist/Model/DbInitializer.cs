using System.Data.Entity;

namespace Artist.Model
{
    public class DbInitializer : IDatabaseInitializer<ArtistDbContext>
    {
        public void InitializeDatabase(ArtistDbContext context)
        {
            return;
        }
    }
}
