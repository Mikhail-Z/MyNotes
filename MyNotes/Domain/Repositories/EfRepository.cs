using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Interfaces;

namespace MyNotes.Domain.Repositories
{
	
	public class EfRepository<T> : IAsyncRepository<T> where T : EntityBase
	{
		protected readonly ContactsContext _dbContext;

		public EfRepository(ContactsContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<int> CreateAsync(Contact contact)
		{
			_dbContext.Contact.Add(contact);
			return _dbContext.SaveChangesAsync();
		}

		public Task<IEnumerable<T>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> criteria)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(Contact contactInfo)
		{
			throw new NotImplementedException();
		}

		/*
		public Task CreateAsync(T entity)
		{
			
			_dbContext.HomePhones.AddRange(contactInfo.HomePhones);
			_dbContext.WorkPhones.AddRange(contactInfo.WorkPhones);
			_dbContext.Skypes.AddRange(contactInfo.SkypeAccounts);
			_dbContext.Emails.AddRange(contactInfo.Emails);
			
			//_dbContext.ContactInfos.AddAsync(contactInfo);
			_dbContext.AddAsync<T>(entity);
			return _dbContext.SaveChangesAsync();
		}

		public Task<T> GetByIdAsync(int id)
		{
		}

		public Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> criteria)
		{
		}

		public Task<bool> UpdateAsync(ContactInfo contactInfo)
		{
			_dbContext.Entry<>().State = ;

			_dbContext.SaveChangesAsync();
		}
		*/
	}
	
}
