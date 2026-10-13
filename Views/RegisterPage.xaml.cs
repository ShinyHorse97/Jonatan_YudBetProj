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
        // Get the name, email, and password from the Entry controls
        var name = UsernameEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the name, email, and password are valid
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DisplayAlert("Error", "Please enter your name, email, and password.", "OK");
            return;
        }
        // Check if the user already exists in the database
        var user = services.Database.UsersList.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            DisplayAlert("Error", "User already exists.", "OK");
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
        DisplayAlert("Success", "User registered successfully.", "OK");
    }
}