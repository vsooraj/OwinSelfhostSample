using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Web.Http;
namespace OwinSelfhostSample
{
    public class Startup
    {
        public Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions();
        }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        // This method is required by Katana:
        //public void Configuration(IAppBuilder app)
        //{
        //    ConfigureAuth(app);
        //    var webApiConfiguration = ConfigureWebApi();
        //    app.UseWebApi(webApiConfiguration);
        //}
        public void Configuration(IAppBuilder appBuilder)
        {

            //enable cors origin requests
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(appBuilder);
            //enable AttributeRoutes
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            appBuilder.UseWebApi(config);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //// Web API configuration and services
            //// Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //// Enable the application to use bearer tokens to authenticate users
            //appBuilder.UseOAuthBearerTokens(OAuthOptions);


            //enable AJ Client 
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
        private void ConfigureAuth(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                // Only do this for demo!!
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(
                    new OAuthBearerAuthenticationOptions());
        }
        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }


    }

}
