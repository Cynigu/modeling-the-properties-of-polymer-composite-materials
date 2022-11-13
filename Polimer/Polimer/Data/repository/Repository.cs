using Microsoft.EntityFrameworkCore;

namespace Polimer.Data.repository
{
    internal class Repository
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public Repository(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
