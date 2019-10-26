using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyNotes.Infrastructure
{
	public class ContactInfoAdapter
	{
		public ContactInfoAdapter() { }

		public ContactInfoAdapter(Contact contactInfo)
		{
			Name = contactInfo.Name;
			HomePhones = ConvertCollectionToViewRepresentation(contactInfo.HomePhones);
			WorkPhones = ConvertCollectionToViewRepresentation(contactInfo.WorkPhones);
			Emails = ConvertCollectionToViewRepresentation(contactInfo.Emails);
			SkypeAccounts = ConvertCollectionToViewRepresentation(contactInfo.SkypeAccounts);
			Comment = contactInfo.Comment;
		}

		public static explicit operator Contact(ContactInfoAdapter contactInfoAdapter)
		{
			return new Contact
			{
				Name = contactInfoAdapter.Name,
				HomePhones = contactInfoAdapter.HomePhones
					.Split(';')
					.Select(x => new HomePhone { Value = x.Trim() })
					.ToArray(),
				WorkPhones = contactInfoAdapter.WorkPhones
					.Split(';')
					.Select(x => new WorkPhone { Value = x.Trim() })
					.ToArray(),
				Emails = contactInfoAdapter.Emails
					.Split(';')
					.Select(x => new Email { Value = x.Trim() })
					.ToArray(),
				SkypeAccounts = contactInfoAdapter.SkypeAccounts
					.Split(';')
					.Select(x => new Skype { Value = x.Trim() })
					.ToArray(),
				BirthDay = contactInfoAdapter.BirthDay,
				Comment = contactInfoAdapter.Comment
			};
		}

		public string Name { get; set; }

		public string HomePhones { get; set; }

		public string WorkPhones { get; set; }

		public string Emails { get; set; }

		public string SkypeAccounts { get; set; }

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
