﻿using DynamicData.Annotations;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using static System.Environment;

namespace ArcInstaller.Models
{
	class ArcInstallerSettings : INotifyPropertyChanged
	{
		/* General */

		// Directory to arc file
		private string _arcDir = "";
		public string ArcDir
		{
			get => _arcDir;
			set
			{
				if (value == _arcDir) return;
				_arcDir = value;
				OnPropertyChanged();
			}
		}

		/* Inject */

		// Path to folder with mods in them
		private string _injectModsFldr;

		public string InjectModsFldr
		{
			get => _injectModsFldr;
			set
			{
				if (value == _injectModsFldr) return;
				_injectModsFldr = value;
				OnPropertyChanged();
			}
		}
		// For Inject: Path to data.arc file to inject mods into or
		// folder for compressed and renamed mods to go in
		private string _injectArcOutput = "";


		public string InjectArcOutput
		{
			get => _injectArcOutput;
			set
			{
				if (value == _injectArcOutput) return;
				_injectArcOutput = value;
				OnPropertyChanged();
			}
		}
		// Determine if mods should be combined into a single
		// folder if dumping them
		private bool _isInjectSingleFldr;


		public bool IsInjectSingleFldr
		{
			get => _isInjectSingleFldr;
			set
			{
				if (value == _isInjectSingleFldr) return;
				_isInjectSingleFldr = value;
				OnPropertyChanged();
			}
		}


		/* FTP */

		// Path to folder with mods in them
		private string _ftpModsFldr = "";


		public string FtpModsFldr
		{
			get => _ftpModsFldr;
			set
			{
				if (value == _ftpModsFldr) return;
				_ftpModsFldr = value;
				OnPropertyChanged();
			}
		}
		// IP address for the switch (give by the ftp client)
		private string _switchIP = "";

		public string SwitchIP
		{
			get => _switchIP;
			set
			{
				if (value == _switchIP) return;
				_switchIP = value;
				OnPropertyChanged();
			}
		}
		// Port number for the switch (given by the ftp client)
		private int _switchPort = 0;

		public int SwitchPort
		{
			get => _switchPort;
			set
			{
				if (value == _switchPort) return;
				_switchPort = value;
				OnPropertyChanged();
			}
		}
		// Path to optional output folder other than default
		// Default: SaltySD/mods <--- this needs to be changed to UltimateModManager/mods
		private string _switchModFldr = "";

		public string SwitchModFldr
		{
			get => _switchModFldr;
			set
			{
				if (value == _switchModFldr) return;
				_switchModFldr = value;
				OnPropertyChanged();
			}
		}
		// Determine if mods should be combined into a single folder
		// on the switch
		private bool _isFtpSingleFldr;
		public bool IsFtpSingleFldr
		{
			get => _isFtpSingleFldr;
			set
			{
				if (value == _isFtpSingleFldr)
					_isFtpSingleFldr = value;
				OnPropertyChanged();
			}
		}


		/* Extract */

		// Path to name table
		private string _nameTable = "";

		public string NameTable
		{
			get => _nameTable;
			set
			{
				if (value == _nameTable) return;
				_nameTable = value;
				OnPropertyChanged();
			}
		}
		// Path to optional output folder
		private string _extractOutputFldr = "";
		public string ExtractOutputFldr
		{
			get => _extractOutputFldr;
			set
			{
				if (value == _extractOutputFldr) return;
				_extractOutputFldr = value;
				OnPropertyChanged();
			}
		}


		public static ArcInstallerSettings Instance { get; } = new ArcInstallerSettings();
		static readonly string _settingsDir = Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "YourCompany", "YourApp");

		static readonly string _settingsPath = Path.Combine(_settingsDir, "settings.json");
		static ArcInstallerSettings()
		{
			Directory.CreateDirectory(_settingsDir);
			if (File.Exists(_settingsPath))
				Instance = JsonConvert.DeserializeObject<ArcInstallerSettings>(File.ReadAllText(_settingsPath));
		}
		public void Save() => File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(this));

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
