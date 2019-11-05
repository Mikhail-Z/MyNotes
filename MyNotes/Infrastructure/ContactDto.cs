using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyNotes.Infrastructure
{
	public class ContactDto
	{
		public ContactDto() { }

		public ContactDto(Contact contact)
		{
			Name = contact.Name;
			//HomePhones = ConvertCollectionToViewRepresentation(contact.HomePhones);
			//WorkPhones = ConvertCollectionToViewRepresentation(contact.WorkPhones);
			//Emails = ConvertCollectionToViewRepresentation(contact.Emails);
			//Skypes = ConvertCollectionToViewRepresentation(contact.Skypes);
			Comment = contact.Comment;
		}

		public static explicit operator Contact(ContactDto contactInfoAdapter)
		{
			return new Contact
			{
				Name = contactInfoAdapter.Name,
				/*HomePhones = contactInfoAdapter.HomePhones?
					.Split(';')
					.Select(x => new HomePhone { Value = x.Trim() })
					.ToArray() ?? new HomePhone[0],
				WorkPhones = contactInfoAdapter.WorkPhones?
					.Split(';')
					.Select(x => new WorkPhone { Value = x.Trim() })
					.ToArray() ?? new WorkPhone[0],
				Emails = contactInfoAdapter.Emails?
					.Split(';')
					.Select(x => new Email { Value = x.Trim() })
					.ToArray() ?? new Email[0],
				Skypes = contactInfoAdapter.Skypes?
					.Split(';')
					.Select(x => new Skype { Value = x.Trim() })
					.ToArray() ?? new Skype[0],*/
				BirthDay = contactInfoAdapter.BirthDay,
				Comment = contactInfoAdapter.Comment
			};
		}

		public string Name { get; set; }

		public string HomePhones { get; set; }

		public string WorkPhones { get; set; }

		public string Emails { get; set; }

		public string Skypes { get; set; }

		public DateTime? BirthDay { get; set; }

		public string Comment { get; set; }

		private string ConvertCollectionToViewRepresentation<T>(T[] collection)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < collection.Count() - 1; i++)
			{
				sb.Append(collection[i] + "; ");
			}
			if (collection.Length > 0)
			{
				sb.Append(collection);
			}

			return sb.ToString();
		}
	}
}
