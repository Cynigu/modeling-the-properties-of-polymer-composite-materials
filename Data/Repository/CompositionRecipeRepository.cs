using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class CompositionRecipeRepository : RepositoryBase<CompositionRecipeEntity>
{
    public CompositionRecipeRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

}