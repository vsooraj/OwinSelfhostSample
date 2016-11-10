using Autofac;
using Autofac.Integration.WebApi;
using OwinSelfhostSample.Controllers;
using OwinSelfhostSample.Models;
using System.Reflection;
using System.Web.Http;

namespace OwinSelfhostSample
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            // ...or you can register individual controlllers manually.
            builder.RegisterType<AccountController>().InstancePerRequest();
            // Register API controller dependencies per request.
            // builder.Register<IAccountRepository>(c => new AccountRepository()).InstancePerRequest();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerRequest();
            builder.RegisterType<ItemRepository>().As<IItemRepository>().InstancePerRequest();
            builder.RegisterType<OperationsRepository>().As<IOperationsRepository>().InstancePerRequest();



            Container = builder.Build();

            return Container;
        }
    }
}
