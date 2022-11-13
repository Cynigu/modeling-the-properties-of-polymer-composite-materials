using Microsoft.EntityFrameworkCore;
using Polimer.Data.Models;
using Polimer.Data.Repository.Models;

namespace Polimer.Data.Repository
{
    public class Repository
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public Repository(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<UserInfo> UserByLoginAndPasswordAsync(string login, string password)
        {
            UserInfo userInfo;
            using ( var context = await _dbContextFactory.CreateDbContextAsync())
            {
                UserEntity? user = await context.Users.FirstAsync(u => u.Login == login && u.Password == password);

                userInfo = new UserInfo()
                {
                    Login = user?.Login,
                    Role = user?.Role,
                    Success = user != null
                };
            }

            return userInfo;
        }
    }
}
