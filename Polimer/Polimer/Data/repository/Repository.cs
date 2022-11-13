using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
