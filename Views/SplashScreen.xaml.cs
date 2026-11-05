using Microsoft.Maui.Graphics;
using System.Diagnostics;

namespace Jonatan_YudBetProj.Views;

public partial class SplashScreen : ContentPage
{
	private static readonly TimeSpan MinimumSplashDuration = TimeSpan.FromSeconds(5);
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

			while (true)
			{
				cancellationToken.ThrowIfCancellationRequested();
				var animationElapsed = Stopwatch.GetElapsedTime(animationStarted);
				var progress = Math.Clamp(
					(float)(animationElapsed.TotalMilliseconds / MinimumSplashDuration.TotalMilliseconds),
					0f,
					1f);
				var sequentialProgress = progress * SectorChartDrawable.SectorCount;

				for (var sector = 0; sector < SectorChartDrawable.SectorCount; sector++)
				{
					var sectorProgress = Math.Clamp(sequentialProgress - sector, 0f, 1f);
					_chartDrawable.SetProgress(sector, SmoothStep(sectorProgress));
				}

				SectorChart.Invalidate();
				if (progress >= 1f)
					break;

				await Task.Delay(33, cancellationToken);
			}

			var totalElapsed = Stopwatch.GetElapsedTime(animationStarted);
			var remainingDisplayTime = MinimumSplashDuration - totalElapsed;
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
	public const int SectorCount = 25;

	private static readonly Color[] SourceColors =
	{
		Color.FromArgb("#F0445B"),
		Color.FromArgb("#16D978"),
		Color.FromArgb("#FFD21F")
	};

	private static readonly Color Purple = Color.FromArgb("#A855F7");
	private readonly Color[] _sectorColors = CreateSectorColors();
	private readonly float[] _progress = new float[SectorCount];

	private static Color[] CreateSectorColors()
	{
		var colors = new Color[SectorCount];
		for (var index = 0; index < colors.Length; index++)
			colors[index] = SourceColors[Random.Shared.Next(SourceColors.Length)];

		return colors;
	}

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
		var gridWidth = Math.Min(340f, Math.Max(0f, width - 8f));
		var gridHeight = Math.Min(68f, Math.Max(24f, height - 12f));
		var left = (width - gridWidth) / 2f;
		var centeredTop = (height - gridHeight) / 2f;
		var top = Math.Max(4f, centeredTop - 10f);
		var cellWidth = gridWidth / SectorCount;
		var radius = gridHeight / 2f;
		var cellRadius = Math.Min(radius, cellWidth / 2f);
		var gridBounds = new RectF(left, top, gridWidth, gridHeight);

		canvas.FillColor = Color.FromArgb("#252530");
		canvas.FillRoundedRectangle(gridBounds, radius);

		for (var index = 0; index < SectorCount; index++)
		{
			var cellLeft = left + (index * cellWidth);
			var bounds = new RectF(cellLeft, top, cellWidth, gridHeight);
			var color = Blend(_sectorColors[index], Purple, _progress[index]);

			canvas.FillColor = color;
			if (index == 0)
			{
				canvas.FillRoundedRectangle(bounds, radius);
				canvas.FillRectangle(new RectF(cellLeft + cellRadius, top, cellWidth - cellRadius, gridHeight));
			}
			else if (index == SectorCount - 1)
			{
				canvas.FillRectangle(new RectF(cellLeft, top, cellWidth - cellRadius, gridHeight));
				canvas.FillRoundedRectangle(bounds, radius);
			}
			else
			{
				canvas.FillRectangle(bounds);
			}

			canvas.FillColor = Color.FromRgba(255, 255, 255, 0.2f);
			canvas.FillRoundedRectangle(
				new RectF(cellLeft + (cellWidth * 0.2f), top + (gridHeight * 0.18f), cellWidth * 0.6f, gridHeight * 0.14f),
				gridHeight * 0.07f);
		}

		canvas.StrokeSize = 1.5f;
		for (var divider = 1; divider < SectorCount; divider++)
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
