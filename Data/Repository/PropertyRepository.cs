using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;
using System.Linq.Expressions;

namespace Polimer.Data.Repository;

public class PropertyRepository : RepositoryBase<PropertyEntity>
{
    public PropertyRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public override async Task AddAsync(PropertyEntity item)
    {

        await using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            context.Units.AttachRange(item.Unit); // !!!!
            await context.Set<PropertyEntity>().AddAsync(item);
            await context.SaveChangesAsync();

        }
    }

    public override async Task<ICollection<PropertyEntity>> GetEntitiesAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var result = await context.Set<PropertyEntity>()
            .Include(x => x.Unit) // !!!!)
            .ToListAsync();

        return result;
    }

    public override async Task<ICollection<PropertyEntity>> GetEntitiesByFilterAsync(
        Func<PropertyEntity, bool> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = context.Set<PropertyEntity>()
            .Include(x => x.Unit)
            .Where(predicate).ToList();

        return result;
    }

    public override async Task<PropertyEntity?> GetEntityByFilterFirstOrDefaultAsync(
        Expression<Func<PropertyEntity, bool>> predicate)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var result = await context.Set<PropertyEntity>()
            .Include(x => x.Unit)
            .FirstOrDefaultAsync(predicate);

        return result;
    }
}