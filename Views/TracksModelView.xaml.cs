using Google.Android.Material.Slider;
using System.Security.Cryptography.X509Certificates;
using Jonatan_YudBetProj.Models;

namespace Jonatan_YudBetProj.Views;

public partial class TracksModelView : ContentPage
{
	public List<RaceTrack> RaceTracks { get; set; }
	public TracksModelView()
	{
		InitializeComponent();
		RaceTracks = services.Database.RaceTracksList;
		BindingContext = this;
	}
}