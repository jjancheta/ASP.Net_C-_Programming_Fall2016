using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment9.Startup))]
namespace Assignment9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
