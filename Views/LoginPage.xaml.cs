using Jonatan_YudBetProj.Models;
using Jonatan_YudBetProj.services;

namespace Jonatan_YudBetProj.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
	public void OnLoginButtonClicked(object sender, EventArgs e)
    {
        //reset all error labels
        EmailErrorLabel.IsVisible = false;
        PasswordErrorLabel.IsVisible = false;
        // Get the email and password from the Entry controls
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the email and password are valid
        if (string.IsNullOrEmpty(email))
        {
            EmailErrorLabel.IsVisible = true;
            EmailErrorLabel.Text = "Email is required.";
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            PasswordErrorLabel.IsVisible = true;
            PasswordErrorLabel.Text = "Password is required.";
            return;
        }
        //check if the email and password are in Database
        if(!Database.UserExists(email, password))
        {
            PasswordErrorLabel.IsVisible = true;
            PasswordErrorLabel.Text = "Invalid email or password.";
            return;
        }
        else
        {
            EmailErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            // Navigate to the FirstPage
            Navigation.PushAsync(new FirstPage());
        }
    }
}