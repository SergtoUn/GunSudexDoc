using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeaponDoc.Startup))]
namespace WeaponDoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
