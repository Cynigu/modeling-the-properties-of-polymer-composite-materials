using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Polimer.Data.factory
{
    internal class ContextFactory : IDbContextFactory<DataContext>
    {
        private readonly string _connectionString;
        private readonly string _fileLogString;

        public ContextFactory(string connectionString, string fileLog)
        {
            _connectionString = connectionString;
            _fileLogString = fileLog;
        }

        public DataContext CreateDbContext()
        {
            //string connectionString = "Data Source = rpkDB.db";
            StreamWriter logStream = new StreamWriter(_fileLogString, true);
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite(_connectionString);
            optionsBuilder.LogTo(logStream.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();

            return new DataContext(optionsBuilder.Options);
        }
    }
}
