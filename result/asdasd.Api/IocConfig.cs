using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using asdasd.Core.CQRS;
using asdasd.Core.Extensions;
using asdasd.Core.Services.Redis;
using asdasd.Core.Services.Sql;
using asdasd.Core.Services.Telegram;
using asdasd.Core.Services.Unique;
using asdasd.Database;
using asdasd.Infrastructure.CQRS;
using asdasd.Infrastructure.Dapper;
using asdasd.Infrastructure.EventHub;
using asdasd.Infrastructure.Identity;
using asdasd.Infrastructure.Unique;
using asdasd.Infrastructure.User;
using asdasd.Infrastructure.Validation;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Serilog;
using StackExchange.Redis;

namespace asdasd.Api
{
    public class IocConfig
    {
        public static void OverrideWithLocalCredentials(ContainerBuilder builder)
        {
            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var credentials = new asdasdaDatabaseCredentials();
                credentials.BuildLocalConnectionString(
                        configuration["Api:Sql:Url"],
                        configuration["Api:Sql:Catalog"]);

                return credentials;
            }).SingleInstance();
        }

        public static void RegisterCredentials(ContainerBuilder builder)
        {
            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var credentails = new IdentityModelCredentials()
                {
                    ClientId = configuration["Auth:Client:Id"],
                    ClientSecret = configuration["Auth:Client:Secret"]
                };

                return credentails;
            }).SingleInstance();

            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var credentails = new RedisCredentials();
                credentails.BuildConnectionString(
                    configuration["Redis:Name"],
                    configuration["Redis:Password"],
                    configuration["Redis:Port"].To<int>(),
                    isSsl: true,
                    allowAdmin: true);

                return ConnectionMultiplexer.Connect(credentails.ConnectionString);
            }).SingleInstance();

            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var credentials = new JobsDatabaseCredentials();
                credentials.BuildConnectionString(
                    configuration["Api:Sql:Url"],
                    configuration["Api:Sql:Port"].To<int>(),
                    configuration["Api:Sql:Catalog"],
                    configuration["Api:Sql:User"],
                    configuration["Api:Sql:Password"]);

                return credentials;
            }).SingleInstance();

            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(configuration["EventHub:ConnectionString"])
                {
                    EntityPath = configuration["EventHub:EntityPath"]
                };

                return connectionStringBuilder;
            }).SingleInstance();
        }

        public static IContainer RegisterDependencies(IServiceCollection services, IHostingEnvironment env, IConfiguration rootConfiguration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            RegisterCredentials(builder);

            if (env.IsDevelopment())
            {
                OverrideWithLocalCredentials(builder);
            }

            builder.Register<IDiscoveryCache>(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var cache = new DiscoveryCache(configuration["Auth:Client:Url"]);
                return cache;
            }).SingleInstance();

            builder.Register(b =>
            {
                return rootConfiguration;
            }).SingleInstance();

            builder.Register(b =>
            {
                var redis = b.Resolve<ConnectionMultiplexer>();
                return redis.GetDatabase();
            }).InstancePerLifetimeScope();

            builder.Register(b =>
            {
                var configuration = b.Resolve<IConfiguration>();
                var redis = b.Resolve<ConnectionMultiplexer>();
                return redis.GetServer(configuration["Redis:Url"]);
            }).InstancePerLifetimeScope();

            builder.RegisterType<IEventDispatcher>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IEventHandler<>)).InstancePerDependency();

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommandHandler<,>)).InstancePerLifetimeScope();

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePerRequest().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IQueryHandler<,>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(AppAbstractValidation<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IReadCacheHandler<,>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IWriteCacheHandler<,>)).InstancePerLifetimeScope();

            builder.Register<ILogger>(b =>
            {
                return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .CreateLogger();
            });

            builder.Register<IClock>(b =>
            {
                return SystemClock.Instance;
            }).SingleInstance();

            builder.Register<IMemoryCache>(b =>
            {
                return new MemoryCache(new MemoryCacheOptions());
            }).SingleInstance();

            builder.RegisterType<DapperConnector<asdasdaDatabaseCredentials>>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GuidService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<IdentityModelClient>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CurrentUserProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RandomService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventHubService>().AsImplementedInterfaces().SingleInstance();

            builder.Register<IClock>(b =>
            {
                return SystemClock.Instance;
            }).SingleInstance();

            builder.Register(b =>
            {
                var connectionStringBuilder = b.Resolve<EventHubsConnectionStringBuilder>();
                return EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            }).InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}