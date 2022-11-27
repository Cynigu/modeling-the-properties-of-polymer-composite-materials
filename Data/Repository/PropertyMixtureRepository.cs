using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class PropertyMixtureRepository : RepositoryBase<PropertyMixtureEntity>
{
    public PropertyMixtureRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    public override async Task AddAsync(PropertyMixtureEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            try
            {
                context.Mixtures.AttachRange(item.Mixture); // !!!!
                context.Properties.AttachRange(item.Property); // !!!!
                await context.Set<PropertyMixtureEntity>().AddAsync(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при дбавлении!" + ex.Message);
            }

        }
    }

    public override async Task<ICollection<PropertyMixtureEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<PropertyMixtureEntity>()
            .Include(x => x.Property) // !!!!
            .Include(x => x.Mixture)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<PropertyMixtureEntity>> GetEntitiesByFilterAsync(
        Func<PropertyMixtureEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<PropertyMixtureEntity>()
            .Include(x => x.Property)
            .Include(x => x.Mixture)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<PropertyMixtureEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<PropertyMixtureEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<PropertyMixtureEntity>()
            .Include(x => x.Property)
            .Include(x => x.Mixture)
            .FirstOrDefaultAsync(predicate);

        return result;
    }
}