using Microsoft.Maui.Graphics;
using System.Diagnostics;

namespace Jonatan_YudBetProj.Views;

public partial class SplashScreen : ContentPage
{
	private readonly SectorChartDrawable _chartDrawable = new();
	private CancellationTokenSource? _animationCancellation;
	private bool _hasNavigated;

	public SplashScreen()
	{
		InitializeComponent();
		SectorChart.Drawable = _chartDrawable;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_animationCancellation?.Cancel();
		_animationCancellation = new CancellationTokenSource();
		_hasNavigated = false;
		_ = AnimateChartAsync(_animationCancellation.Token);
	}

	protected override void OnDisappearing()
	{
		_animationCancellation?.Cancel();
		_animationCancellation?.Dispose();
		_animationCancellation = null;
		base.OnDisappearing();
	}

	private async Task AnimateChartAsync(CancellationToken cancellationToken)
	{
		try
		{
			var animationStarted = Stopwatch.GetTimestamp();
			_chartDrawable.Reset();
			SectorChart.Invalidate();

			await Task.Delay(80, cancellationToken);
			for (var sector = 0; sector < 3; sector++)
			{
				for (var frame = 0; frame <= 16; frame++)
				{
					cancellationToken.ThrowIfCancellationRequested();
					_chartDrawable.SetProgress(sector, SmoothStep(frame / 16f));
					SectorChart.Invalidate();
					await Task.Delay(12, cancellationToken);
				}

				await Task.Delay(35, cancellationToken);
			}

			var elapsed = Stopwatch.GetElapsedTime(animationStarted);
			var remainingDisplayTime = TimeSpan.FromSeconds(10) - elapsed;
			if (remainingDisplayTime > TimeSpan.Zero)
			{
				await Task.Delay(remainingDisplayTime, cancellationToken);
			}

			if (!_hasNavigated && Shell.Current is not null)
			{
				_hasNavigated = true;
				await Shell.Current.GoToAsync(nameof(WelcomePage));
			}
		}
		catch (OperationCanceledException)
		{
			// The page is no longer visible.
		}
	}

	private static float SmoothStep(float value)
	{
		return value * value * (3f - (2f * value));
	}
}

internal sealed class SectorChartDrawable : IDrawable
{
	private static readonly Color[] SourceColors =
	{
		Color.FromArgb("#FFD21F"),
		Color.FromArgb("#16D978"),
		Color.FromArgb("#F0445B")
	};

	private static readonly Color Purple = Color.FromArgb("#A855F7");
	private readonly float[] _progress = new float[3];

	public void Reset()
	{
		Array.Clear(_progress);
	}

	public void SetProgress(int sector, float progress)
	{
		_progress[sector] = Math.Clamp(progress, 0f, 1f);
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		var width = dirtyRect.Width;
		var height = dirtyRect.Height;
		var gridWidth = Math.Min(300f, width);
		var gridHeight = Math.Min(44f, height * 0.55f);
		var left = (width - gridWidth) / 2f;
		var top = (height - gridHeight) / 2f;
		var cellWidth = gridWidth / 3f;
		var radius = gridHeight / 2f;
		var gridBounds = new RectF(left, top, gridWidth, gridHeight);

		canvas.FillColor = Color.FromArgb("#252530");
		canvas.FillRoundedRectangle(gridBounds, radius);

		for (var index = 0; index < 3; index++)
		{
			var cellLeft = left + (index * cellWidth);
			var bounds = new RectF(cellLeft, top, cellWidth, gridHeight);
			var color = Blend(SourceColors[index], Purple, _progress[index]);

			canvas.FillColor = color;
			if (index == 0)
			{
				canvas.FillRoundedRectangle(bounds, radius);
				canvas.FillRectangle(new RectF(cellLeft + radius, top, cellWidth - radius, gridHeight));
			}
			else if (index == 2)
			{
				canvas.FillRectangle(new RectF(cellLeft, top, cellWidth - radius, gridHeight));
				canvas.FillRoundedRectangle(bounds, radius);
			}
			else
			{
				canvas.FillRectangle(bounds);
			}

			canvas.FillColor = Color.FromRgba(255, 255, 255, 0.2f);
			canvas.FillRoundedRectangle(
				new RectF(cellLeft + (gridHeight * 0.2f), top + (gridHeight * 0.18f), cellWidth - (gridHeight * 0.4f), gridHeight * 0.14f),
				gridHeight * 0.07f);
		}

		canvas.StrokeColor = Color.FromArgb("#C0C4CC");
		canvas.StrokeSize = 2.5f;
		canvas.DrawRoundedRectangle(gridBounds, radius);

		canvas.StrokeSize = 1.5f;
		for (var divider = 1; divider < 3; divider++)
		{
			var dividerX = left + (divider * cellWidth);
			canvas.DrawLine(dividerX, top + 2f, dividerX, top + gridHeight - 2f);
		}
	}

	private static Color Blend(Color from, Color to, float amount)
	{
		return new Color(
			from.Red + ((to.Red - from.Red) * amount),
			from.Green + ((to.Green - from.Green) * amount),
			from.Blue + ((to.Blue - from.Blue) * amount),
			1f);
	}
}
