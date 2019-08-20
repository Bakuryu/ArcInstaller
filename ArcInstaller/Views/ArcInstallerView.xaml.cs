using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArcInstaller.Views
{
	public class ArcInstallerView : UserControl
	{
		public ArcInstallerView()
		{
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
