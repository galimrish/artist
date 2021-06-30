namespace Artist.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ArtistDbContext : DbContext
    {
        public ArtistDbContext()
            : base("name=ArtistDbContext")
        {
        }

        public virtual DbSet<MasterCategory> MasterCategory { get; set; }
        public virtual DbSet<Master> Master { get; set; }
        public virtual DbSet<MasterRequest> MasterRequest { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<ActionHistory> ActionHistory { get; set; }
        public virtual DbSet<MasterRequestComment> MasterRequestComment { get; set; }
    }
}
