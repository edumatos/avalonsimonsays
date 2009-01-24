using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace AvalonSimonSays.Code
{
	[Script]
	public class SimonCanvas : Canvas
	{
		public const int DefaultWidth = 600;
		public const int DefaultHeight = 480;

		public SimonCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;


			var a = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/01.png").ToSource()
			}.AttachTo(this);

			var Blue = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/02.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this);

			var Red = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/03.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this);

			var Green = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/04.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this);


			var Yellow = new Image
			{
				Stretch = Stretch.Fill,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Source = (Assets.Shared.KnownAssets.Path.Assets + "/05.png").ToSource(),
				Visibility = Visibility.Hidden
			}.AttachTo(this);


			var BlueOverlay = new Rectangle
			{
				Fill = Brushes.Blue,
				Opacity = 0,
				Cursor = Cursors.Hand
			}.MoveTo(91, 172).SizeTo(193, 162).AttachTo(this);

			BlueOverlay.MouseLeftButtonDown +=
				delegate
				{
					Blue.Show();
				};
			BlueOverlay.MouseLeftButtonUp +=
				delegate
				{
					Blue.Hide();

					// green sound
					(Assets.Shared.KnownAssets.Path.Sounds + "/4.mp3").PlaySound();
				};


			
		

			var RedOverlay = new Rectangle
			{
				Fill = Brushes.Red,
				Opacity = 0,
				Cursor = Cursors.Hand
			}.MoveTo(98, 40).SizeTo(193, 120).AttachTo(this);

			RedOverlay.MouseLeftButtonDown +=
				delegate
				{
					Red.Show();
				};
			RedOverlay.MouseLeftButtonUp +=
				delegate
				{
					Red.Hide();

					(Assets.Shared.KnownAssets.Path.Sounds + "/5.mp3").PlaySound();
				};



			var GreenOverlay = new Rectangle
			{
				Fill = Brushes.Green,
				Opacity = 0,
				Cursor = Cursors.Hand
			}.MoveTo(309, 44).SizeTo(190, 114).AttachTo(this);

			GreenOverlay.MouseLeftButtonDown +=
				delegate
				{
					Green.Show();
				};
			GreenOverlay.MouseLeftButtonUp +=
				delegate
				{
					Green.Hide();

					(Assets.Shared.KnownAssets.Path.Sounds + "/3.mp3").PlaySound();
				};

			var YellowOverlay = new Rectangle
			{
				Fill = Brushes.Yellow,
				Opacity = 0,
				Cursor = Cursors.Hand
			}.MoveTo(309, 171).SizeTo(190, 174).AttachTo(this);

			YellowOverlay.MouseLeftButtonDown +=
				delegate
				{
					Yellow.Show();
				};
			YellowOverlay.MouseLeftButtonUp +=
				delegate
				{
					Yellow.Hide();

					(Assets.Shared.KnownAssets.Path.Sounds + "/1.mp3").PlaySound();
				};
		}
	}
}
