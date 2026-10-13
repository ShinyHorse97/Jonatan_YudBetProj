namespace Jonatan_YudBetProj.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
	public void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Get the email and password from the Entry controls
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        // Check if the email and password are valid
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DisplayAlert("Error", "Please enter your email and password.", "OK");
            return;
        }
        // Check if the user exists in the database
        var user = services.Database.UsersList.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            DisplayAlert("Error", "Invalid email or password.", "OK");
            return;
        }
        // Navigate to the FirstPage
        Navigation.PushAsync(new FirstPage());
    }
}