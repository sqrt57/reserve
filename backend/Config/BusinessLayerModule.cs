﻿using Autofac;
using backend.BusinessLogic;
using backend.DbStores;
using backend.Models;
using backend.Security;
using backend.Services;
using Microsoft.AspNetCore.Identity;

namespace backend.Config;

public class BusinessLayerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserIdAccessor>().AsSelf();

        builder.RegisterType<DapperConnections>().AsSelf().InstancePerLifetimeScope();

        builder.RegisterType<MarsUserStore>().As<IUserStore<ApplicationUser>>().InstancePerLifetimeScope();
        builder.RegisterType<MarsRoleStore>().As<IRoleStore<ApplicationRole>>().InstancePerLifetimeScope();

        builder.RegisterType<VisitorsStore>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<TariffsStore>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ProductsStore>().AsSelf().InstancePerLifetimeScope();

        builder.RegisterType<TariffsService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<VisitorsService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ProductsService>().AsSelf().InstancePerLifetimeScope();
    }
}
