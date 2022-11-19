using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class MixtureRepository : RepositoryBase<MixtureEntity>
{
    public MixtureRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}