using Jonatan_YudBetProj.Views;

namespace Jonatan_YudBetProj
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(TracksModelView), typeof(TracksModelView));
            Routing.RegisterRoute(nameof(RacecarCRUD), typeof(RacecarCRUD));
        }
    }
}
