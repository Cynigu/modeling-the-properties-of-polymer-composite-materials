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
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            UserEntity? user = await context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);

            var userInfo = new UserInfo()
            {
                Login = user?.Login,
                Role = user?.Role,
                Success = user != null
            };

            return userInfo;
        }

        public async Task<ICollection<UserEntity>> GetUsersAsync()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            ICollection<UserEntity> users = await context.Users.ToListAsync();

            return users;
        }

        public async Task AddUserAsync(string login, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException();
            }

            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if (user != null)
                    throw new Exception("Такой пользователь уже существует!");

                context.Users.Add(new UserEntity()
                {
                    Login = login,
                    Role = role,
                    Password = password
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
