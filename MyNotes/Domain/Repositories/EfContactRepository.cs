using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Interfaces;

namespace MyNotes.Domain.Repositories
{
	public class EfContactRepository : IAsyncRepository<Contact>
	{
		protected readonly ContactContext _dbContext;

		public EfContactRepository(ContactContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<int> CreateAsync(Contact contact)
		{
			_dbContext.Contact.Add(contact);
			return _dbContext.SaveChangesAsync();
		}

		public Task<Contact> GetByIdAsync(int id)
		{
			return _dbContext.Contact
				.Include(c => c.HomePhones)
				.Include(c => c.WorkPhones)
				.Include(c => c.Emails)
				.Include(c => c.Skypes)
				.FirstAsync(c => c.Id == id);
		}

		public async Task<int> RemoveAsync(Contact contact)
		{
			//_dbContext.Entry(contact).State = EntityState.Deleted;
			var contactFromDb = await GetByIdAsync(contact.Id);
			_dbContext.Contact.Remove(contactFromDb);
			return await _dbContext.SaveChangesAsync();
		}

		public Task<IEnumerable<Contact>> SearchAsync(Expression<Func<Contact, bool>> criteria)
		{
			return Task.FromResult(
				_dbContext.Contact
					.AsQueryable<Contact>()
					.Where<Contact>(criteria)
					.Include(c => c.HomePhones)
					.Include(c => c.WorkPhones)
					.Include(c => c.Emails)
					.Include(c => c.Skypes)
					.AsEnumerable());
		}

		public Task<IEnumerable<Contact>> GetAll()
		{
			return Task.FromResult(
				_dbContext.Contact.AsQueryable()
					.Include(c => c.HomePhones)
					.Include(c => c.WorkPhones)
					.Include(c => c.Emails)
					.Include(c => c.Skypes).AsEnumerable());
		}

		public Task<int> UpdateAsync(Contact newContact)
		{
			var oldContact = _dbContext.Contact.Find(newContact.Id);
			
			oldContact.Name = newContact.Name;
			oldContact.BirthDay = newContact.BirthDay;
			oldContact.Skypes = newContact.Skypes;
			oldContact.HomePhones = newContact.HomePhones;
			oldContact.WorkPhones = newContact.WorkPhones;
			oldContact.Emails = newContact.Emails;
			oldContact.Comment = newContact.Comment;
			
			return _dbContext.SaveChangesAsync();
		}
	}
}
