using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Infrastructure.Configuration
{
	public static class ServiceProviderFactory
	{
		private static IServiceProvider _serviceProvider;

		public static IServiceProvider ServiceProvider
		{
			set
			{
				if (_serviceProvider != null)
				{
					throw new ArgumentException();
				}
				_serviceProvider = value;
			}
			get
			{
				return _serviceProvider;
			}
		}
	}
}
