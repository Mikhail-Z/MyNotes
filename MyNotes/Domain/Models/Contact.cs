using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyNotes.Domain.Entities
{
	//[Table("Contact")]
	public class Contact : EntityBase
	{
		public string Name { get; set; }

		public ICollection<HomePhone> HomePhones { get; set; }

		public ICollection<WorkPhone> WorkPhones { get; set; }

		public ICollection<Email> Emails { get; set; }

		public ICollection<Skype> Skypes { get; set; }

		public DateTime? BirthDay { get; set; }

		public string Comment { get; set; }
	}
}
