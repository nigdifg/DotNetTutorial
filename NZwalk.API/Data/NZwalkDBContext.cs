using Microsoft.EntityFrameworkCore;

namespace NZwalk.API.Data
{
    public class NZwalkDBContext: DbContext
    {
        public NZwalkDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
        }
        public DbSet<Model.Domain.Difficulty> Difficulties { get; set; }
        public DbSet<Model.Domain.Region> Regions { get; set; }
        public DbSet<Model.Domain.Walk> Walks { get; set; }

    }
}
