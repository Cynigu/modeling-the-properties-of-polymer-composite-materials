using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class CompositionRecipeRepository : RepositoryBase<CompositionRecipeEntity>
{
    public CompositionRecipeRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(CompositionRecipeEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {

            try
            {
                context.Mixtures.AttachRange(item.Mixture); // !!!!
                context.Recipes.AttachRange(item.Recipe); // !!!!
                await context.Set<CompositionRecipeEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Выбериите разные материалы!");
            }

        }
    }

    public override async Task<ICollection<CompositionRecipeEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<CompositionRecipeEntity>()
            .Include(x => x.Recipe) // !!!!
            .Include(x => x.Mixture)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<CompositionRecipeEntity>> GetEntitiesByFilterAsync(
        Func<CompositionRecipeEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<CompositionRecipeEntity>()
            .Include(x => x.Recipe)
            .Include(x => x.Mixture)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<CompositionRecipeEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<CompositionRecipeEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<CompositionRecipeEntity>()
            .Include(x => x.Recipe)
            .Include(x => x.Mixture)
            .FirstOrDefaultAsync(predicate);

        return result;
    }
}