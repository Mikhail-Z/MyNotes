using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Interfaces;
using MyNotes.Domain.Repositories;
using MyNotes.Infrastructure;
using MyNotes.Infrastructure.Configuration;
using MyNotes.Services;
using MyNotes.Services.Interfaces;
using MyNotes.ViewModels;
using MyNotes.Views;
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

			WindowManager.Register(typeof(ContactVM), ModelAction.Add, typeof(AddContactWindow));
			WindowManager.Register(typeof(ContactVM), ModelAction.Change, typeof(EditContactWindow));
			WindowManager.Register(typeof(ContactVM), ModelAction.Show, typeof(ShowContactWindow));
			WindowManager.Register(typeof(ContactVM), ModelAction.Remove, typeof(RemoveContactWindow));

			ServiceProvider = serviceCollection.BuildServiceProvider();
			ServiceProviderFactory.ServiceProvider = ServiceProvider;
		}

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}

		private void ConfigureServices(IServiceCollection services)
		{
			var provider = services
					.AddEntityFrameworkSqlite()
					.BuildServiceProvider();
			/*
			services.AddDbContext<ContactContext>(options =>
			{
				options.UseSqlite(@"Data Source=C:\Users\mikhail\sqlite_databases\MyNotes.db;Mode=ReadWriteCreate;");
				options.UseInternalServiceProvider(provider);
			});
			*/

			services.AddLogging(config =>
			{
				config.AddConsole();
			});

			services.AddSingleton<AlphabetSearchManager>(new AlphabetSearchManager("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
			services.AddSingleton<IAsyncRepository<Contact>, EfContactRepository>();
			services.AddSingleton<IContactManagerService, ContactManagerService>();
			services.AddSingleton<MainViewVM>();
			services.AddSingleton<MainWindow>();
		}
	}
}
