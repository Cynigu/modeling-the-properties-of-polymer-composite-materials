using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class CompatibilityMaterialrRepository : RepositoryBase<CompatibilityMaterialEntity>
{
    public CompatibilityMaterialrRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

}