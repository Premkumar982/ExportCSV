using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExportCSV.Startup))]
namespace ExportCSV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
