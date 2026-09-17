namespace Jonatan_YudBetProj.Views;
using Models;
public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
		
		foreach (var user in new UsersList().Users)
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
    // get the list of users from the UsersList class and display them in the ListView
}