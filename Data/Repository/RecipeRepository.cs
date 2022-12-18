using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class RecipeRepository : RepositoryBase<RecipeEntity>
{
    public RecipeRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(RecipeEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.CompatibilitiesMaterial.AttachRange(item.CompatibilityMaterial); // !!!!
                context.Additives.AttachRange(item.Additive); // !!!!
                await context.Set<RecipeEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении! " + ex.Message);
            }

        }
    }

    public override async Task<ICollection<RecipeEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<RecipeEntity>()
            .Include(x => x.Additive) // !!!!
            .Include(x => x.CompatibilityMaterial)
            .Include(x => x.CompatibilityMaterial.FirstMaterial)
            .Include(x => x.CompatibilityMaterial.SecondMaterial)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<RecipeEntity>> GetEntitiesByFilterAsync(
        Func<RecipeEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<RecipeEntity>()
            .Include(x => x.Additive)
            .Include(x => x.CompatibilityMaterial)
            .Include(x => x.CompatibilityMaterial.FirstMaterial)
            .Include(x => x.CompatibilityMaterial.SecondMaterial)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<RecipeEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<RecipeEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<RecipeEntity>()
            .Include(x => x.Additive)
            .Include(x => x.CompatibilityMaterial)
            .Include(x => x.CompatibilityMaterial.FirstMaterial)
            .Include(x => x.CompatibilityMaterial.SecondMaterial)
            .FirstOrDefaultAsync(predicate);

        return result;
    }


    public override async Task UpdateAsync(RecipeEntity item)
    {
        RecipeEntity i = new RecipeEntity()
        {
            Id = item.Id,
            CompatibilityMaterial = item.CompatibilityMaterial,
            Additive = item.Additive,
            IdCompatibilityMaterial = item.CompatibilityMaterial.Id,
            IdAdditive = item.Additive.Id,
            ContentMaterialFirst = item.ContentMaterialFirst,
            ContentMaterialSecond = item.ContentMaterialSecond,
            ContentAdditive = item.ContentAdditive,
        };
        await base.UpdateAsync(i);
    }
}