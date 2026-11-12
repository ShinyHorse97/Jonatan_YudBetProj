using Jonatan_YudBetProj.services;
using Jonatan_YudBetProj.ViewModels;

namespace Jonatan_YudBetProj.Views;

public partial class RegisterPage : ContentPage
{
    public bool IsFormValid = true;
    public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}