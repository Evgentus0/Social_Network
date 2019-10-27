using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Cors;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(SocialNetwork_PL.Startup))]

namespace SocialNetwork_PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();  

            ConfigureAuth(app);

        }
    }
}
