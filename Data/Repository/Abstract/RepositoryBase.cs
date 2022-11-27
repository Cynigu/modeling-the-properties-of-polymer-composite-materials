using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using System.Linq.Expressions;

namespace Polimer.Data.Repository.Abstract
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IDbContextFactory<DataContext> _dbContextFactory;

        protected RepositoryBase(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual async Task AddAsync(TEntity item)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            await context.Set<TEntity>().AddAsync(item);
            await context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(ICollection<TEntity> items)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            await context.Set<TEntity>().AddRangeAsync(items);
            await context.SaveChangesAsync();
        }

        public virtual async Task<ICollection<TEntity>> GetEntitiesAsync()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var result = await context.Set<TEntity>().ToListAsync();

            return result;
        }
        public virtual async Task<ICollection<TEntity>> GetEntitiesByFilterAsync(Func<TEntity, bool> predicate)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var result = context.Set<TEntity>().Where(predicate).ToList();

            return result;
        }
        public virtual async Task<TEntity?> GetEntityByFilterFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var result = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> RemoveRangeAsync(Func<TEntity, bool> predicate)
        {
            try
            {
                await using var context = await _dbContextFactory.CreateDbContextAsync();
                var entities = context.Set<TEntity>().Where(predicate);

                var removeRangeAsync = entities as TEntity[] ?? entities.ToArray();
                context.Set<TEntity>().RemoveRange(removeRangeAsync);
                await context.SaveChangesAsync();
                return removeRangeAsync;
            }
            catch (Exception e)
            {
                throw new Exception("Эта рецептура привязана к составу рецептуры. Удалите ее из состава рецептуры!");
            }
            
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
