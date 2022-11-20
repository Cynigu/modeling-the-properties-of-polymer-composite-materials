using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class CompatibilityMaterialrRepository : RepositoryBase<CompatibilityMaterialEntity>
{
    public CompatibilityMaterialrRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(CompatibilityMaterialEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            if (item.FirstMaterial.Equals(item.SecondMaterial))
            {
                context.Materials.AttachRange(item.FirstMaterial); // !!!!
            }
            else
            {
                context.Materials.AttachRange(item.FirstMaterial, item.SecondMaterial); // !!!!
            }
            try
            {
                await context.Set<CompatibilityMaterialEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Выбериите разные материалы!");
            }
           
        }
    }

    public override async Task<ICollection<CompatibilityMaterialEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
       
        var result = await context.Set<CompatibilityMaterialEntity>()
            .Include(x => x.FirstMaterial) // !!!!
            .Include(x => x.SecondMaterial)
            .ToListAsync();

        return result;
    }
    
    public override async Task<ICollection<CompatibilityMaterialEntity>> GetEntitiesByFilterAsync(
        Func<CompatibilityMaterialEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<CompatibilityMaterialEntity>()
            .Include(x => x.FirstMaterial)
            .Include(x => x.SecondMaterial)
            .Where(predicate).ToList();

        return result;
    }
    
    public override async Task<CompatibilityMaterialEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<CompatibilityMaterialEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<CompatibilityMaterialEntity>()
            .Include(x => x.FirstMaterial)
            .Include(x => x.SecondMaterial)
            .FirstOrDefaultAsync(predicate);

        return result;
    }
}