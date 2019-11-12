using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using MyNotes.Utils;

namespace MyNotes.Infrastructure
{
	public class ContactDto
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public string HomePhones { get; set; }
		public string WorkPhones { get; set; }
		public string Emails { get; set; }
		public string Skypes { get; set; }
		public string BirthDay { get; set; }
		public string Comment { get; set; }

		public ContactDto() { }

		public ContactDto(Contact contact)
		{
			var newContact = ReplaceNullWithEmptyArray(contact);

			Id = newContact.Id;
			Name = newContact.Name;
			BirthDay = newContact.BirthDay.HasValue ? newContact.BirthDay.Value.ToString("dd.MM.yyyy") : null;
			HomePhones = ConvertCollectionToViewRepresentation(newContact.HomePhones);
			WorkPhones = ConvertCollectionToViewRepresentation(newContact.WorkPhones);
			Emails = ConvertCollectionToViewRepresentation(newContact.Emails);
			Skypes = ConvertCollectionToViewRepresentation(newContact.Skypes);
			Comment = newContact.Comment;
		}

		private Contact ReplaceNullWithEmptyArray(Contact contact)
		{
			return new Contact
			{
				Id = contact.Id,
				Name = contact.Name,
				BirthDay = contact.BirthDay,
				HomePhones = contact.HomePhones ?? (new HomePhone[0]),
				Emails = contact.Emails ?? (new Email[0]),
				WorkPhones = contact.WorkPhones ?? (new WorkPhone[0]),
				Skypes = contact.Skypes ?? (new Skype[0]),
				Comment = contact.Comment
			};
		}

		public static explicit operator Contact(ContactDto contactDto)
		{
			var isBirthDayDateTime = DateTime.TryParseExact(
				contactDto.BirthDay, 
				"dd.MM.yyyy", 
				CultureInfo.CurrentCulture, 
				DateTimeStyles.AssumeLocal, 
				out var birthDayDateTime);

			return new Contact
			{
				Id = contactDto.Id,
				Name = contactDto.Name,
				HomePhones = ValueStringToArray<HomePhone>(contactDto.HomePhones),
				WorkPhones = ValueStringToArray<WorkPhone>(contactDto.WorkPhones),
				Emails = ValueStringToArray<Email>(contactDto.Emails),
				Skypes = ValueStringToArray<Skype>(contactDto.Skypes),
				BirthDay = isBirthDayDateTime ? new DateTime?(birthDayDateTime) : null,
				Comment = contactDto.Comment
			};
		}
		
		private static List<T> ValueStringToArray<T>(string value) where T : ValueBase, new()
		{
			return StringUtils.InsignificantStringToNull(value)?
					.Split(';')
					.Select(x => new T { Value = x.Trim() })
					.ToList() ?? new List<T>();
		}

		private string ConvertCollectionToViewRepresentation<T>(ICollection<T> collection) where T : ValueBase
		{
			var array = collection.ToArray();
			if (array.Length == 0)
			{
				return null;
			}

			var sb = new StringBuilder();
			for (int i = 0; i < array.Count() - 1; i++)
			{
				sb.Append(array[i].Value + "; ");
			}
			sb.Append(array[array.Length-1].Value);

			return sb.ToString();
		}
	}
}
