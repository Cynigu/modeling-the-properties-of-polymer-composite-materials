using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;

namespace Polimer.Data
{
    public sealed class DataContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<MaterialEntity> Materials { get; set; } = null!;
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
