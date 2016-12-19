using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment8.Startup))]
namespace Assignment8
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
