using Autofac;
using AutoMapper;
using Polimer.App.Profilers;
using System.Reflection;
using System;
using System.Linq;
using Module = Autofac.Module;
using System.Collections.Generic;

namespace Polimer.App.Autofac;

internal class AutoMapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        var assemblyType = typeof(AdditiveProfile).GetTypeInfo();
        builder.RegisterAssemblyTypes(assemblyType.Assembly)
            .Where(t => t.Name.EndsWith("Profile"))
            .As<Profile>();


        builder.Register(context => new MapperConfiguration(cfg =>
        {
            foreach (var profile in context.Resolve<IEnumerable<Profile>>())
            {
                cfg.AddProfile(profile);
            }
        })).AsSelf().SingleInstance();

        //builder.Register(ctx => new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile(new UserProfiler());
        //    cfg.AddProfile(new MaterialProfile());
        //    cfg.AddProfile(new AdditiveProfiler());
        //    cfg.AddProfile(new CompatibilityMaterialProfiler());
        //}));

        builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve))
            .As<IMapper>().InstancePerLifetimeScope();
    }

}