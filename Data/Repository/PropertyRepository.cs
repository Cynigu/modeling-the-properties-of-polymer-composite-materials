using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class PropertyRepository : RepositoryBase<PropertyEntity>
{
    public PropertyRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}