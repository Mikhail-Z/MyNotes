using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Services.Interfaces
{
	public interface IContactsSearchService
	{
		Contact Search(string value);
	}
}
