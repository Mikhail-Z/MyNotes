using Microsoft.Extensions.Logging;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Interfaces;
using MyNotes.Domain.Repositories;
using MyNotes.Infrastructure;
using MyNotes.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyNotes.Services
{
	public class ContactManagerService : IContactManagerService
	{
		private IAsyncRepository<Contact> _repository;
		private ILogger<ContactManagerService> _logger;
		private const int MAX_DAYS_NUMBER_WHEN_BIRTHDAY_IS_SOON = 14;

		public ContactManagerService(
			IAsyncRepository<Contact> repository, 
			ILogger<ContactManagerService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<int> UpdateContact(ContactDto contactDto)
		{
			try
			{
				return await _repository.UpdateAsync((Contact)contactDto);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return -1;
			}
		}

		public async Task<IEnumerable<ContactDto>> GetContactsByQuery(string searchQuery, bool isBirthdaySoon, char? firstLetter=null)
		{
			Expression<Func<Contact, bool>> searchExpression = (Contact c) => (c.Name.Contains(searchQuery) ||
				(firstLetter.HasValue ? c.Name.StartsWith(firstLetter.Value) : false)) ||
				/*(c.HomePhones.Select(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.WorkPhones.Select(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.Emails.Select(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.Skypes.Select(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||*/
				isBirthdaySoon ? (c.BirthDay < DateTime.Today.AddDays(MAX_DAYS_NUMBER_WHEN_BIRTHDAY_IS_SOON) && c.BirthDay >= DateTime.Today) : true;
			try
			{
				return (await _repository.SearchAsync(searchExpression))
					.Select(c => new ContactDto(c));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return default;
			}
		}

		public async Task<IEnumerable<ContactDto>> GetAllContacts(bool isBirthdaySoon)
		{
			try
			{
				if (isBirthdaySoon)
				{
					Expression<Func<Contact, bool>> searchExpression = (Contact c) =>
						(c.BirthDay < DateTime.Today.AddDays(MAX_DAYS_NUMBER_WHEN_BIRTHDAY_IS_SOON)
						&& c.BirthDay >= DateTime.Today);
					return (await _repository.SearchAsync(searchExpression))
						.Select(c => new ContactDto(c));
				}
				else
				{
					return (await _repository.GetAll()).Select(c => new ContactDto(c));
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return new ContactDto[0];
			}
		}

		public async Task<int> RemoveContact(ContactDto contact)
		{
			try
			{
				return await _repository.RemoveAsync((Contact)contact);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return -1;
			}
		}

		public async Task<int> AddContact(ContactDto contact)
		{
			try
			{
				return await _repository.CreateAsync((Contact)contact);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return -1;
			}
		}
	}
}
