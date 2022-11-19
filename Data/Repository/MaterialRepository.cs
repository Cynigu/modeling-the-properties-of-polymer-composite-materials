using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class MaterialRepository : RepositoryBase<MaterialEntity>
{
    public MaterialRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}