using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.Data.Repository
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
