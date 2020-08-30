using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebTestSecirity.Startup))]
namespace WebTestSecirity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
