using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vsslabs.Hrms.Startup))]
namespace Vsslabs.Hrms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
