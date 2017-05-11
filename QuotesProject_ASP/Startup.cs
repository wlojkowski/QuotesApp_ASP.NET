using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuotesProject_ASP.Startup))]
namespace QuotesProject_ASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
