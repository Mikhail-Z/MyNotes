using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Interfaces
{
	public interface IAsyncRepository<T> where T : EntityBase
	{
		Task<T> GetByIdAsync(int id);

		Task<int> CreateAsync(Contact contactInfo);

		Task<bool> UpdateAsync(Contact contactInfo);

		Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> criteria);

		Task<IEnumerable<T>> GetAll();
	}
}
