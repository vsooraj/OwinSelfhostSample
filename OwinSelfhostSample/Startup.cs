using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using OwinSelfhostSample.Controllers;
using OwinSelfhostSample.Models;
using System.Reflection;
using System.Web.Http;




namespace OwinSelfhostSample
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions();
            //Bootstrapper.Run();

        }
        public void Configuration(IAppBuilder appBuilder)
        {

            //enable cors origin requests
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            appBuilder.UseOAuthBearerTokens(OAuthOptions);
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Autofac Start
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new AccountRepository()).As<IAccountRepository>().InstancePerRequest();
            builder.Register(c => new ItemRepository()).As<IItemRepository>().InstancePerRequest();
            builder.Register(c => new OperationsRepository()).As<IOperationsRepository>().InstancePerRequest();


            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            //Autofac End
            appBuilder.UseWebApi(config);


            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //appBuilder.CreatePerOwinContext(ApplicationDbContext.Create);
            //appBuilder.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Configure the application for OAuth based flow
            //PublicClientId = "self";
            //var myProvider = new ApplicationOAuthProvider();
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    //TokenEndpointPath = new PathString("/Token"),
            //    //// Provider = new ApplicationOAuthProvider(PublicClientId),
            //    //AuthorizeEndpointPath = new PathString("/api/Account/Token"),
            //    //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    //// In production mode set AllowInsecureHttp = false
            //    //AllowInsecureHttp = true,
            //    //Provider = myProvider

            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = myProvider
            //};

            //// Enable the application to use bearer tokens to authenticate users
            //appBuilder.UseOAuthBearerTokens(OAuthOptions);
            //appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());



            //config.Filters.Add(new BasicAuthenticationAttribute());

            const string rootFolder = @"..\..\miGuardClient";
            var fileSystem = new PhysicalFileSystem(rootFolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };

            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };
            appBuilder.UseFileServer(options);
        }



    }

}
