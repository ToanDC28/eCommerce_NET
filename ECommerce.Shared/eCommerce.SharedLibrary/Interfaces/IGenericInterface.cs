using eCommerce.SharedLibrary.Responses;
using System.Linq.Expressions;

namespace eCommerce.SharedLibrary.Interfaces;

public interface IGenericInterface<T> where T : class
{
    Task<Response> CreateAsync(T entity);
    Task<Response> UpdateAsync(T entity);
    Task<Response> DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByID(int id);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
}
