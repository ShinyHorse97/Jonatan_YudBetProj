using Jonatan_YudBetProj.ViewModels;

namespace Jonatan_YudBetProj.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}