using MyNotes.Domain.Entities;
using MyNotes.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Services.Interfaces
{
	public interface IContactManagerService
	{
		Task<IEnumerable<ContactDto>> GetAllContacts(bool isBirthdaySoon);

		Task<IEnumerable<ContactDto>> GetContactsByQuery(string searchQuery, bool isBirthdaySoon, char? firstLetter = null);

		Task<int> UpdateContact(ContactDto contact);

		Task<int> RemoveContact(ContactDto contact);

		Task<int> AddContact(ContactDto contact);
	}
}
