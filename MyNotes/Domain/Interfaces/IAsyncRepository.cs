using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Interfaces
{
	public interface IAsyncRepository<T> //where T : EntityBase
	{
		Task<T> GetByIdAsync(int id);

		Task<int> CreateAsync(Contact contact);

		Task<int> UpdateAsync(Contact contact);

		Task<int> RemoveAsync(Contact contact);

		Task<IEnumerable<T>> SearchAsync(Expression<Func<Contact, bool>> criteria);

		Task<IEnumerable<T>> GetAll();
	}
}
