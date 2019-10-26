using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Domain.Repositories
{
	public class ContactsContext : DbContext
	{
		public DbSet<Contact> Contact { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Skype> Skypes { get; set; }
		public DbSet<HomePhone> HomePhones { get; set; }
		public DbSet<WorkPhone> WorkPhones { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(
				@"Data Source=MyNotes.db;Version=3;");
		}
	}
}
