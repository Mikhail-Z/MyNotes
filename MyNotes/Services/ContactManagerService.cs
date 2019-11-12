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
			_logger.LogInformation("Some message");
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

		public async Task<IEnumerable<ContactDto>> GetContactsByQuery(
			string searchQuery, bool isBirthDaySoon, char? firstLetter=null)
		{
			Func<Contact, bool> searchQueryExpr =
				(Contact c) => String.IsNullOrEmpty(searchQuery) ||
					(c.Name.ToUpper().Contains(searchQuery.ToUpper()) ||
					(c.HomePhones.Where(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.WorkPhones.Where(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.Emails.Where(phone => phone.Value.Contains(searchQuery)).Count() > 0) ||
				(c.Skypes.Where(phone => phone.Value.Contains(searchQuery)).Count() > 0));

			Func<Contact, bool> searchByLetterExpr = 
				(Contact c) => !firstLetter.HasValue ||
					c.Name.ToUpper().StartsWith(firstLetter.Value.ToString().ToUpper());

			Func<Contact, bool> isBirthDaySoonExpr =
				(Contact c) => !isBirthDaySoon || c.BirthDay.HasValue &&
				new DateTime(
					DateTime.Today.Year, 
					c.BirthDay.Value.Month, 
					c.BirthDay.Value.Day) < DateTime.Today.AddDays(MAX_DAYS_NUMBER_WHEN_BIRTHDAY_IS_SOON)
				&& c.BirthDay >= DateTime.Today;

			try
			{
				return (await _repository.GetAll())
					.Where(c => searchQueryExpr(c) && searchByLetterExpr(c) && isBirthDaySoonExpr(c))
					.Select(c => new ContactDto(c));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return default;
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
