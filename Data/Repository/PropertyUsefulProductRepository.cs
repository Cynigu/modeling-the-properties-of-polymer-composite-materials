using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class PropertyUsefulProductRepository : RepositoryBase<PropertyUsefulProductEntity>
{
    public PropertyUsefulProductRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(PropertyUsefulProductEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.UsefulProductes.AttachRange(item.UsefulProduct); // !!!!
                context.Properties.AttachRange(item.Property); // !!!!
                await context.Set<PropertyUsefulProductEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при дбавлении!" + ex.Message);
            }

        }
    }

    public override async Task<ICollection<PropertyUsefulProductEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<PropertyUsefulProductEntity>()
            .Include(x => x.Property) // !!!!
            .Include(x => x.UsefulProduct)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<PropertyUsefulProductEntity>> GetEntitiesByFilterAsync(
        Func<PropertyUsefulProductEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<PropertyUsefulProductEntity>()
            .Include(x => x.Property)
            .Include(x => x.UsefulProduct)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<PropertyUsefulProductEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<PropertyUsefulProductEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<PropertyUsefulProductEntity>()
            .Include(x => x.Property)
            .Include(x => x.UsefulProduct)
            .FirstOrDefaultAsync(predicate);

        return result;
    }

    public override async Task UpdateAsync(PropertyUsefulProductEntity item)
    {
        PropertyUsefulProductEntity i = new PropertyUsefulProductEntity()
        {
            Id = item.Id,
            Property = item.Property,
            UsefulProduct = item.UsefulProduct,
            IdProperty = item.Property.Id,
            IdUsefulProduct = item.UsefulProduct.Id,
            Value = item.Value
        };
        await base.UpdateAsync(i);
    }
}