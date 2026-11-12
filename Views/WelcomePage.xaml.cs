namespace Jonatan_YudBetProj.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.WelcomePageViewModel();
    }
}