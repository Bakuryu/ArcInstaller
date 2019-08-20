using ArcInstaller.Models;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Text;

namespace ArcInstaller.ViewModels
{

	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			DoTheThing = ReactiveCommand.Create(RunTheThingAsync);
		}

		public ReactiveCommand<Unit, Unit> DoTheThing { get; }

		async void RunTheThingAsync()
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Title = "Title",
				Filters = new List<FileDialogFilter> { }

			};
			FileDialogFilter arcFilter = new FileDialogFilter();
			arcFilter.Extensions.Add("arc");
			arcFilter.Name = "Arc File";
			dialog.Filters.Add(arcFilter);
			string[] files = await dialog.ShowAsync(Application.Current.MainWindow);
			if (files != null && files.Length > 0)
			{
				ArcInstallerSettings.Instance.ArcDir = files[0];
				ArcInstallerSettings.Instance.Save();
			}
		}
	}
}
