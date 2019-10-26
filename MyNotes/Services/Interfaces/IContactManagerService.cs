using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Services.Interfaces
{
	public interface IContactManagerService
	{
		IEnumerable<Contact> GetAllContacts();

		void ChangeContact(Contact contactInfo);

		void RemoveContact(Contact contactInfo);
	}
}
