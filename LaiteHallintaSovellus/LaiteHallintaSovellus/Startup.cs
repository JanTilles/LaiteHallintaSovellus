using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaiteHallintaSovellus.Startup))]
namespace LaiteHallintaSovellus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
