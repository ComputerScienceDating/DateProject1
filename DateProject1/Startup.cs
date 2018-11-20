using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DateProject1.Startup))]
namespace DateProject1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
