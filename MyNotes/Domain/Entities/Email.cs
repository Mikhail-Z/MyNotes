using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Domain.Entities
{
	public class Email : ValueBase
	{
		public Contact Contact { get; set; }
	}
}
