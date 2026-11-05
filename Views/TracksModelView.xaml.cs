
using Jonatan_YudBetProj.Models;

namespace Jonatan_YudBetProj.Views;

public partial class TracksModelView : ContentPage
{
	private const double CardWidth = 225;
	private const double HorizontalSpacing = 12;
	private const double PageHorizontalPadding = 32;

	public List<RaceTrack> RaceTracks { get; set; }
	public TracksModelView()
	{
		InitializeComponent();
		RaceTracks = services.Database.RaceTracksList;
		BindingContext = this;
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);

		if (width <= 0)
			return;

		var availableWidth = width - PageHorizontalPadding;
		var maximumCardsPerRow = Math.Max(1, (int)((availableWidth + HorizontalSpacing) /
			(CardWidth + HorizontalSpacing)));

		if (TracksLayout.Span != maximumCardsPerRow)
			TracksLayout.Span = maximumCardsPerRow;
	}
}