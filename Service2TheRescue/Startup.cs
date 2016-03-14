using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Service2TheRescue.Startup))]
namespace Service2TheRescue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
