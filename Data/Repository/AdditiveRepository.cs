using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class AdditiveRepository : RepositoryBase<AdditiveEntity>
{
    public AdditiveRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

}