using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelxIdentity.Startup))]
namespace HotelxIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
