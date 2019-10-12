using System;
using System.Threading.Tasks;
using ManageShop.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ManageShop.App_Start.StartupConfig))]

namespace ManageShop.App_Start
{
    public class StartupConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // Configure the db context, user manager and signin manager to use a single instance per request
            
        }
    }
}
