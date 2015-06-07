using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoFit.Startup))]
namespace GoFit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
