﻿using Avalonia;
using Avalonia.Markup.Xaml;

namespace ArcInstaller
{
	public class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
