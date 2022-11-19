using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class PropertyMixtureRepository : RepositoryBase<PropertyMixtureEntity>
{
    public PropertyMixtureRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

}