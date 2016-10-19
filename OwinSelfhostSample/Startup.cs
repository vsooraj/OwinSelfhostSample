using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;

namespace OwinSelfhostSample
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //// config.EnableQuerySupport();
            //config.Routes.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            //config.EnsureInitialized();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            appBuilder.UseWebApi(config);

            const string rootFolder = @"..\..\miGuardClient";
            var fileSystem = new PhysicalFileSystem(rootFolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };
            //options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "index.html" };
            appBuilder.UseFileServer(options);

        }

        //private static IEdmModel GetEdmModel()
        //{
        //    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
        //    builder.Namespace = "OwinSelfhostSample";
        //    builder.ContainerName = "DefaultContainer";
        //    builder.EntitySet<Product>("Product");
        //    var edmModel = builder.GetEdmModel();
        //    return edmModel;
        //}
    }

}
