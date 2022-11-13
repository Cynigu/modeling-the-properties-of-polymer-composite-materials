using Autofac;
using Microsoft.EntityFrameworkCore;
using Polimer.Data;
using Polimer.Data.factory;
using Polimer.Data.repository;

namespace Polimer.Autofac
{
    internal class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ContextFactory(@"Data Source = db.db", @"logs.txt"))
                .As<IDbContextFactory<DataContext>>();
            builder.RegisterType<Repository>().AsSelf();
        }
    }
}
