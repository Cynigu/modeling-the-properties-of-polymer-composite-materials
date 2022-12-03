using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class PropertyMaterialRepository : RepositoryBase<PropertyMaterialEntity>
{
    public PropertyMaterialRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(PropertyMaterialEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.Materials.AttachRange(item.Material); // !!!!
                context.Properties.AttachRange(item.Property); // !!!!
                await context.Set<PropertyMaterialEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при дбавлении!" + ex.Message);
            }

        }
    }

    public override async Task<ICollection<PropertyMaterialEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<PropertyMaterialEntity>()
            .Include(x => x.Property) // !!!!
            .Include(x => x.Material)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<PropertyMaterialEntity>> GetEntitiesByFilterAsync(
        Func<PropertyMaterialEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<PropertyMaterialEntity>()
            .Include(x => x.Property)
            .Include(x => x.Material)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<PropertyMaterialEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<PropertyMaterialEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<PropertyMaterialEntity>()
            .Include(x => x.Property)
            .Include(x => x.Material)
            .FirstOrDefaultAsync(predicate);

        return result;
    }


    public override async Task UpdateAsync(PropertyMaterialEntity item)
    {
        PropertyMaterialEntity i = new PropertyMaterialEntity()
        {
            Id = item.Id,
            Property = item.Property,
            Material = item.Material,
            IdProperty = item.Property.Id,
            IdMaterial = item.Material.Id,
            Value = item.Value
        };
        await base.UpdateAsync(i);
    }
}