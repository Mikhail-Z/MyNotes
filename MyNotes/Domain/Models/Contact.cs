using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyNotes.Domain.Entities
{
	[Table("Contact")]
	public class Contact : EntityBase
	{
		[Required, StringLength(50)]
		public string Name { get; set; }

		public HomePhone[] HomePhones { get; set; }

		public WorkPhone[] WorkPhones { get; set; }

		public Email[] Emails { get; set; }

		public Skype[] SkypeAccounts { get; set; }

		public DateTime? BirthDay { get; set; }

		public string Comment { get; set; }
	}
}
