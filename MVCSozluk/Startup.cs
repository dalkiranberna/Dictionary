using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSozluk.Startup))]
namespace MVCSozluk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
