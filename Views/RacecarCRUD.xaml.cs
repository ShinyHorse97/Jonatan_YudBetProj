namespace Jonatan_YudBetProj.Views;

public partial class RacecarCRUD : ContentPage
{
	public RacecarCRUD()
	{
		InitializeComponent();
		BindingContext = new ViewModels.RacecarCRUDViewModel();
    }
}