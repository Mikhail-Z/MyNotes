using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyNotes.Domain.Entities
{
	public abstract class ValueBase : EntityBase
	{
		public string Value { get; set; }
	}
}
