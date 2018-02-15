using BluTimesheet.Services.implementations;
using BluTimesheet.Services.interfaces;
using System.Linq;
using System.Web.Http;
using Unity.AspNet.WebApi;
using BluTimesheet.Utils;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using Microsoft.Owin.Security.OAuth;
using Unity;

namespace BluTimesheet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            //Dependecy Injection
            var container = new UnityContainer();
            container.RegisterType<IActivityTypeService, ActivityTypeService>();
            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<IProjectTypeService, ProjectTypeService>();
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IProjectRoleTypeService, ProjectRoleTypeService>();
            container.RegisterType<IUserInfoService, UserInfoService>();
            config.DependencyResolver = new UnityDependencyResolver(container);

            //Global validation
            config.Filters.Add(new ValidateModelAttribute());

            //Global Authorization
            config.Filters.Add(new AuthorizeAttribute());

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }


            );
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        
        }
    }
}
