using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoldenChicken.Startup))]
namespace GoldenChicken
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
