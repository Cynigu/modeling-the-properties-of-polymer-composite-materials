using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;

namespace Polimer.Data.Repository
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(IDbContextFactory<DataContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
