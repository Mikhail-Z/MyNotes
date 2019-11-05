using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Domain.Repositories
{
	public class ContactContext : DbContext
	{
		public DbSet<Contact> Contact { get; set; }
		public DbSet<Email> Emails { get; set; }
		public DbSet<Skype> Skypes { get; set; }
		public DbSet<HomePhone> HomePhones { get; set; }
		public DbSet<WorkPhone> WorkPhones { get; set; }

		public ContactContext(DbContextOptions options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Contact>(e => e.HasIndex(prp => prp.Name).IsUnique());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"Data Source=C:\Users\mikhail\sqlite_databases\MyNotes.db;Mode=ReadWriteCreate;");
		}
	}
}
