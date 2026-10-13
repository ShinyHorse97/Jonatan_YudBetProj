namespace Jonatan_YudBetProj.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
    public void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the LoginPage
        Navigation.PushAsync(new LoginPage());
    }
    public void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the RegisterPage
        Navigation.PushAsync(new RegisterPage());
    }
}