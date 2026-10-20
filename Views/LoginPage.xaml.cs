using Jonatan_YudBetProj.Models;
using Jonatan_YudBetProj.services;

namespace Jonatan_YudBetProj.Views;

public partial class LoginPage : ContentPage
{
    public bool IsFormValid = true;
    public LoginPage()
	{
		InitializeComponent();
	}
	public void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // reset validation state and error labels
        IsFormValid = true;
        EmailErrorLabel.Text = string.Empty;
        EmailErrorLabel.IsVisible = false;
        PasswordErrorLabel.Text = string.Empty;
        PasswordErrorLabel.IsVisible = false;
        // Get the email and password from the Entry controls
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the email and password are valid
        if (string.IsNullOrEmpty(email))
        {
            EmailErrorLabel.Text = "Email is required.";
            EmailErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        if (string.IsNullOrEmpty(password))
        {
            PasswordErrorLabel.Text = "Password is required.";
            PasswordErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        //check if the email and password are in Database
        if(!Database.UserExists(email, password))
        {
            PasswordErrorLabel.Text = "Invalid email or password.";
            PasswordErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        else if(IsFormValid)
        {
            EmailErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            // Navigate to the FirstPage
            Navigation.PushAsync(new FirstPage());
        }
    }

    public void OnRegisterTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}