namespace Jonatan_YudBetProj.Views;
public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
        //foreach user in UsersList in the database, create a horizontal stack layout with the user's name, email, and image and add it to the DataLayout
        foreach (var user in services.Database.UsersList)
        {
            var layout = new HorizontalStackLayout
            {
                Spacing = 10,
            };
            var NameLabel = new Label
            {
                Text = user.Name,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center
            };
            var EmailLabel = new Label
            {
                Text = user.Email,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center
            };
            var userImage = new Image
            {
                Source = user.ImageSRC,
                WidthRequest = 150,
                HeightRequest = 150,
                VerticalOptions = LayoutOptions.Center
            };
            layout.Children.Add(userImage);
            layout.Children.Add(NameLabel);
            layout.Children.Add(EmailLabel);

            DataLayout.Children.Add(layout);
        }
    }
    // get the list of users from the Database and display them in the ListView
}