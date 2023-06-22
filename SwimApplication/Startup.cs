using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SwimApplication.Startup))]
namespace SwimApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
