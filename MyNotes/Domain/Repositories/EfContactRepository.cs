using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Interfaces;
using MyNotes.Infrastructure;

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

		public Task<IEnumerable<Contact>> GetAll()
		{
			return Task.FromResult(_dbContext.Contact.AsEnumerable());
		}

		public Task<Contact> GetByIdAsync(int id)
		{
			return _dbContext.Contact.FindAsync(id).AsTask();
		}

		public Task<int> RemoveAsync(Contact contact)
		{
			_dbContext.Contact.Remove(contact);
			return _dbContext.SaveChangesAsync();
		}

		public Task<IEnumerable<Contact>> SearchAsync(Expression<Func<Contact, bool>> criteria)
		{
			return Task.FromResult(
				_dbContext.Contact
					.AsQueryable<Contact>()
					.Where<Contact>(criteria)
					.AsEnumerable());
		}

		public Task<int> UpdateAsync(Contact newContact)
		{
			var oldContact = _dbContext.Contact.Find(newContact.Id);
			//oldContact.HomePhones = newContact.HomePhones;
			//oldContact.WorkPhones = newContact.WorkPhones;
			//oldContact.Emails = newContact.Emails;
			oldContact.BirthDay = newContact.BirthDay;
			oldContact.Comment = newContact.Comment;
			oldContact.Name = newContact.Name;
			return _dbContext.SaveChangesAsync();
		}

	}
	
}
