using Autofac;
using Microsoft.EntityFrameworkCore;
using Polimer.Data.Factory;

namespace Polimer.Data.Autofac
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ContextFactory(@"Data Source = polim.db", @"logs.txt"))
                .As<IDbContextFactory<DataContext>>();
            builder.RegisterType<Repository.Repository>().AsSelf();
        }
    }
}
