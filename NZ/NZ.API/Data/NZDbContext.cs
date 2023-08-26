using Microsoft.EntityFrameworkCore;
using NZ.API.Domain.Models;

namespace NZ.API.Data
{
    public class NZDbContext : DbContext
    {
        public NZDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
