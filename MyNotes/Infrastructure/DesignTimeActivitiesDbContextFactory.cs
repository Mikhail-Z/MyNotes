using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyNotes.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Infrastructure
{
	class DesignTimeActivitiesDbContextFactory : IDesignTimeDbContextFactory<ContactContext>
	{
		public ContactContext CreateDbContext(string[] args)
		{
			DbContextOptionsBuilder<ContactContext> builder = new DbContextOptionsBuilder<ContactContext>();

			var context = new ContactContext(
				builder
				.UseSqlite(@"Data Source=C:\Users\mikhail\sqlite_databases\MyNotes.db;Mode=ReadWriteCreate;")
				.Options);

			return context;
		}
	}
}
