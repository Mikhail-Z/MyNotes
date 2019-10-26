using Microsoft.Extensions.DependencyInjection;
using MyNotes.Domain.Entities;
using MyNotes.ViewModels;
using MyNotes.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyNotes
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public IServiceProvider ServiceProvider { get; private set; }

		public App()
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			WindowManager.Register(typeof(Contact), ModelAction.Add, typeof(AddContactInfoWindow));
			WindowManager.Register(typeof(Contact), ModelAction.Change, typeof(ChangeContactInfoWindow));
			WindowManager.Register(typeof(Contact), ModelAction.Choose, typeof(ShowContactInfoWindow));
			WindowManager.Register(typeof(Contact), ModelAction.Remove, typeof(RemoveContactInfoWindow));

			ServiceProvider = serviceCollection.BuildServiceProvider();
		}

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}

		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<MainViewVM>();
			services.AddSingleton<MainWindow>();
		}
	}
}
