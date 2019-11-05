using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Domain.Entities
{
	public class WorkPhone : ValueBase
	{
		public Contact Contact { get; set; }
	}
}
