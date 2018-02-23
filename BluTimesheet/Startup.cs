using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using BluTimesheet.Authorization;

[assembly: OwinStartup(typeof(BluTimesheet.Startup))]

namespace BluTimesheet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();            
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            app.UseWebApi(config);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }        
    }
}
