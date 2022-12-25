using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class UsefulProductRepository : RepositoryBase<UsefulProductEntity>
{
    public UsefulProductRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(UsefulProductEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.Recipes.AttachRange(item.Recipe); // !!!!
                await context.Set<UsefulProductEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при дбавлении!" + ex.Message);
            }

        }
    }

    public override async Task<ICollection<UsefulProductEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<UsefulProductEntity>()
            .Include(x => x.Recipe) // !!!!
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<UsefulProductEntity>> GetEntitiesByFilterAsync(
        Func<UsefulProductEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<UsefulProductEntity>()
            .Include(x => x.Recipe) // !!!!
            .Include(x => x.Recipe.CompatibilityMaterial)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<UsefulProductEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<UsefulProductEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<UsefulProductEntity>()
            .Include(x => x.Recipe) // !!!!
            .FirstOrDefaultAsync(predicate);

        return result;
    }

    public override async Task UpdateAsync(UsefulProductEntity item)
    {
        UsefulProductEntity i = new UsefulProductEntity()
        {
            Id = item.Id,
            Recipe = item.Recipe,
            Name = item.Name,
            IdRecipe = item.IdRecipe,
        };
        await base.UpdateAsync(i);
    }
}