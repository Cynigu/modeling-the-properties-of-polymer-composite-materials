using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;

namespace Polimer.Data
{
    public sealed class DataContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<MaterialEntity> Materials { get; set; } = null!;
        public DbSet<MixtureEntity> Mixtures { get; set; } = null!;
        public DbSet<UnitEntity> Units { get; set; } = null!;
        public DbSet<PropertyEntity> Properties { get; set; } = null!;
        public DbSet<PropertyMaterialEntity> PropertiesMaterial { get; set; } = null!;
        public DbSet<PropertyMixtureEntity> PropertiesMixture { get; set; } = null!;
        public DbSet<CompatibilityMaterialEntity> CompatibilitiesMaterial{ get; set; } = null!;
        public DbSet<AdditiveEntity> Additives { get; set; } = null!;
        public DbSet<RecipeEntity> Recipes { get; set; } = null!;
        public DbSet<CompositionRecipeEntity> CompositionRecipes { get; set; } = null!;
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
