using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DTS.Startup))]
namespace DTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
