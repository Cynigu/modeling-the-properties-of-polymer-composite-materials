using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class PropertyAdditiveRepository : RepositoryBase<PropertyAdditiveEntity>
{
    public PropertyAdditiveRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public override async Task AddAsync(PropertyAdditiveEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.Additives.AttachRange(item.Additive); // !!!!
                context.Properties.AttachRange(item.Property); // !!!!
                await context.Set<PropertyAdditiveEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при дбавлении!" + ex.Message);
            }

        }
    }

    public override async Task<ICollection<PropertyAdditiveEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<PropertyAdditiveEntity>()
            .Include(x => x.Property) // !!!!
            .Include(x => x.Additive)
            .Include(x => x.Property.Unit)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<PropertyAdditiveEntity>> GetEntitiesByFilterAsync(
        Func<PropertyAdditiveEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<PropertyAdditiveEntity>()
            .Include(x => x.Property)
            .Include(x => x.Additive)
            .Include(x => x.Property.Unit)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<PropertyAdditiveEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<PropertyAdditiveEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<PropertyAdditiveEntity>()
            .Include(x => x.Property)
            .Include(x => x.Additive)
            .Include(x => x.Property.Unit)
            .FirstOrDefaultAsync(predicate);

        return result;
    }


    public override async Task UpdateAsync(PropertyAdditiveEntity item)
    {
        PropertyAdditiveEntity i = new PropertyAdditiveEntity()
        {
            Id = item.Id,
            Property = item.Property,
            Additive = item.Additive,
            IdProperty = item.Property.Id,
            IdAdditive = item.Additive.Id,
            Value = item.Value
        };
        await base.UpdateAsync(i);
    }
}