using Jonatan_YudBetProj.services;

namespace Jonatan_YudBetProj.Views;

public partial class RegisterPage : ContentPage
{
    public bool IsFormValid = true;
    public RegisterPage()
	{
		InitializeComponent();
	}

    // navigate back to Login page when user taps Login link
    public void OnLoginTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage());
    }
	public void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // reset validation state and error labels
        IsFormValid = true;
        UsernameErrorLabel.Text = string.Empty;
        UsernameErrorLabel.IsVisible = false;
        EmailErrorLabel.Text = string.Empty;
        EmailErrorLabel.IsVisible = false;
        PasswordErrorLabel.Text = string.Empty;
        PasswordErrorLabel.IsVisible = false;
        ConfirmPasswordErrorLabel.Text = string.Empty;
        ConfirmPasswordErrorLabel.IsVisible = false;
        // Get the name, email, and password from the Entry controls
        var name = UsernameEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the name, email, and password are valid
        if (string.IsNullOrEmpty(name))
        {
            UsernameErrorLabel.Text = "Name cannot be empty.";
            UsernameErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        if (string.IsNullOrEmpty(email))
        {
            EmailErrorLabel.Text = "Email cannot be empty.";
            EmailErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        if (string.IsNullOrEmpty(password))
        {
            PasswordErrorLabel.Text = "Password cannot be empty.";
            PasswordErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        //add regex to check if the name is valid, it can contain numbers
        var nameRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9\s]{2,15}$");
        if (!string.IsNullOrEmpty(name) && !nameRegex.IsMatch(name))
        {
            UsernameErrorLabel.Text = "Name must be between 2 and 15 characters and contain only letters, numbers, and spaces.";
            UsernameErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        //add regex to check if the email is valid
        var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!string.IsNullOrEmpty(email) && !emailRegex.IsMatch(email))
        {
            EmailErrorLabel.Text = "Email format is incorrect.";
            EmailErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        //add regex to check if the password is valid
        var passwordRegex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        if (!string.IsNullOrEmpty(password) && !passwordRegex.IsMatch(password))
        {
            PasswordErrorLabel.Text = "Password must be at least 8 characters long and contain at least one letter and one number.";
            PasswordErrorLabel.IsVisible = true;
            IsFormValid = false;
        }

        if (string.IsNullOrEmpty(ConfirmPasswordEntry.Text) || PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            ConfirmPasswordErrorLabel.Text = "Passwords do not match.";
            ConfirmPasswordErrorLabel.IsVisible = true;
            IsFormValid = false;
        }
        // Check if the user already exists in the database
        var user = services.Database.UsersList.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            DisplayAlertAsync("Error", "User already exists.", "OK");
            return;
        }
        else if (IsFormValid)
        {
            // Create a new user and add it to the database
            var newUser = new Models.Users()
            {
                Name = name,
                Email = email,
                Password = password,
                ImageSRC = "https://cdn-icons-png.flaticon.com/512/149/149071.png"
            };
            services.Database.UsersList.Add(newUser);
            DisplayAlertAsync("Success", "User registered successfully.", "ok");
            // clear error labels visibility after successful register
            UsernameErrorLabel.IsVisible = false;
            EmailErrorLabel.IsVisible = false;
            PasswordErrorLabel.IsVisible = false;
            ConfirmPasswordErrorLabel.IsVisible = false;
        }
    }
}