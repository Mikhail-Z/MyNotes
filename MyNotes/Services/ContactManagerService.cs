using Microsoft.Extensions.Logging;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Services
{
	public class ContactManagerService : IContactManagerService
	{
		private EfRepository<Contact> _repository;

		public ContactManagerService(
			EfRepository<Contact> repository, 
			ILogger<ContactManagerService> logger)
		{
			_repository = repository;
		}

		public void ChangeContact(Contact contactInfo)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Contact> GetAllContacts()
		{
			throw new NotImplementedException();
		}

		public void RemoveContact(Contact contactInfo)
		{
			throw new NotImplementedException();
		}
	}
}
