using Autofac;
using Microsoft.EntityFrameworkCore;
using Polimer.Data.Factory;
using Polimer.Data.Repository;

namespace Polimer.Data.Autofac
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ContextFactory(@"Data Source = polim.db", @"logs.txt"))
                .As<IDbContextFactory<DataContext>>();
            builder.RegisterType<UserRepository>().AsSelf();
            builder.RegisterType<MaterialRepository>().AsSelf();
            builder.RegisterType<MixtureRepository>().AsSelf();
            builder.RegisterType<UnitRepository>().AsSelf();
            builder.RegisterType<PropertyRepository>().AsSelf();
            builder.RegisterType<PropertyMaterialRepository>().AsSelf();
            builder.RegisterType<PropertyMixtureRepository>().AsSelf();
            builder.RegisterType<CompatibilityMaterialrRepository>().AsSelf();
            builder.RegisterType<AdditiveRepository>().AsSelf();
            builder.RegisterType<RecipeRepository>().AsSelf();
            builder.RegisterType<CompositionRecipeRepository>().AsSelf();
        }
    }
}
