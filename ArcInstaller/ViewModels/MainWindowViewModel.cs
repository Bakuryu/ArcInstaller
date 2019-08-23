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
		//int TabStartIndex = 2;
		public MainWindowViewModel()
		{
			TabStartIndex = GetStartIndex();
			SelectFile = ReactiveCommand.Create<string[]>(OpenFileDialogAsync);
			SelectFolder = ReactiveCommand.Create<string>(OpenFolderDialogAsync);
		}

		public ReactiveCommand<string[], Unit> SelectFile { get; }
		public ReactiveCommand<string, Unit> SelectFolder { get; }
		public int TabStartIndex { get; }

		async void OpenFileDialogAsync(string[] openFileParams)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Title = "Title",
				Filters = new List<FileDialogFilter>()

			};
			if (openFileParams != null && openFileParams.Length > 1)
			{
				FileDialogFilter fileFilter = new FileDialogFilter();
				fileFilter.Extensions.Add(openFileParams[0]);
				fileFilter.Name = openFileParams[1];
				dialog.Filters.Add(fileFilter);
			}
			string[] files = await dialog.ShowAsync(Application.Current.MainWindow);
			if (files != null && files.Length > 0)
			{
				if (openFileParams != null && openFileParams.Length > 0)
				{
					SaveFileSettings(files[0], openFileParams[openFileParams.Length - 1]);
					ArcInstallerSettings.Instance.Save();
				}
			}
		}


		async void OpenFolderDialogAsync(string settingName)
		{

			OpenFolderDialog dialog = new OpenFolderDialog
			{
				Title = "Test"

			};
			string folder = await dialog.ShowAsync(Application.Current.MainWindow);
			if (folder != "")
			{
				if (settingName != "")
				{
					SaveFolderSettings(folder, settingName);
					ArcInstallerSettings.Instance.Save();
				}
			}
		}

		void SaveFileSettings(string file, string settingName)
		{
			switch (settingName)
			{
				case "ArcDir":
					ArcInstallerSettings.Instance.ArcDir = file;
					break;
					//case "NameTable":
					//	ArcInstallerSettings.Instance.NameTable = file;
					//	break;

			}
		}

		void SaveFolderSettings(string folder, string settingName)
		{
			switch (settingName)
			{
				case "CompressModsFldr":
					ArcInstallerSettings.Instance.CompressModsFldr = folder;
					break;
				case "CompressOutputFldr":
					ArcInstallerSettings.Instance.CompressOutputFldr = folder;
					break;
				case "FtpModsFldr":
					ArcInstallerSettings.Instance.FtpModsFldr = folder;
					break;
				case "SwitchModFldr":
					ArcInstallerSettings.Instance.SwitchModFldr = folder;
					break;
			}
		}

		void ToggleCheckBox(string settingName)
		{
			switch (settingName)
			{
				case "IsCompressSingleFldr":
					ArcInstallerSettings.Instance.IsCompressSingleFldr = !ArcInstallerSettings.Instance.IsCompressSingleFldr;
					break;
				case "IsFtpSingleFldr":
					ArcInstallerSettings.Instance.IsFtpSingleFldr = !ArcInstallerSettings.Instance.IsFtpSingleFldr;
					break;
			}
			ArcInstallerSettings.Instance.Save();
		}

		int GetStartIndex()
		{
			int startIndex = 2;

			if (ArcInstallerSettings.Instance.CompressModsFldr != "" && ArcInstallerSettings.Instance.CompressOutputFldr != "")
			{
				startIndex = 0;
			}
			else if (ArcInstallerSettings.Instance.FtpModsFldr != "")
			{
				startIndex = 1;
			}

			return startIndex;
		}
	}
}
