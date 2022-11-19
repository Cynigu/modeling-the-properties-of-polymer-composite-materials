using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository;

public class RecipeRepository : RepositoryBase<RecipeEntity>
{
    public RecipeRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
    {
    }

}