using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreFinalQuiz1_LaoM.Startup))]
namespace PreFinalQuiz1_LaoM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
