using Jonatan_YudBetProj.services;

namespace Jonatan_YudBetProj.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
	public void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        //reset error labels
        UsernameErrorLabel.IsVisible = false;
        EmailErrorLabel.IsVisible = false;
        PasswordErrorLabel.IsVisible = false;
        ConfirmPasswordErrorLabel.IsVisible = false;
        // Get the name, email, and password from the Entry controls
        var name = UsernameEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the name, email, and password are valid
        if (string.IsNullOrEmpty(name))
        {
            UsernameErrorLabel.IsVisible = true;
            UsernameErrorLabel.Text = "Name cannot be empty.";
            return;
        }
        //add regex to check if the name is valid, it can contain numbers
        var nameRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9\s]{2,100}$");
        if (!nameRegex.IsMatch(name))
        {
            UsernameErrorLabel.IsVisible = true;
            UsernameErrorLabel.Text = "Name must be between 2 and 100 characters and contain only letters, numbers, and spaces.";
            return;
        }

        if (string.IsNullOrEmpty(email))
        {
            EmailErrorLabel.IsVisible = true;
            EmailErrorLabel.Text = "Email cannot be empty.";
            return;
        }
        //add regex to check if the email is valid
        var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
        {
            EmailErrorLabel.IsVisible = true;
            EmailErrorLabel.Text = "email format incorrect";
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            PasswordErrorLabel.IsVisible = true;
            PasswordErrorLabel.Text = "Password cannot be empty.";
            return;
        }
        //add regex to check if the password is valid
        var passwordRegex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        if (!passwordRegex.IsMatch(password))
        {
            PasswordErrorLabel.IsVisible = true;
            PasswordErrorLabel.Text = "Password format Incorrect";
            return;
        }

        if (string.IsNullOrEmpty(ConfirmPasswordEntry.Text) || PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            ConfirmPasswordErrorLabel.IsVisible = true;
            ConfirmPasswordErrorLabel.Text = "Passwords do not match.";
            return;
        }
        // Check if the user already exists in the database
        var user = services.Database.UsersList.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            DisplayAlertAsync("Error", "User already exists.", "OK");
            return;
        }
        // Create a new user and add it to the database
        var newUser = new Models.Users()
        {
            Name = name,
            Email = email,
            Password = password,
            ImageSRC = "https://cdn-icons-png.flaticon.com/512/149/149071.png"
        };
        services.Database.UsersList.Add(newUser);
        DisplayAlertAsync("Success", "User registered successfully.", "Go to Login");
        Navigation.PushAsync((new LoginPage()));
    }
}