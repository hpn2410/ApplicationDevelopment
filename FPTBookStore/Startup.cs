using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTBookStore.Startup))]
namespace FPTBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
