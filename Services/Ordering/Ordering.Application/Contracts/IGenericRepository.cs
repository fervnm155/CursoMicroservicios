using System.Linq.Expressions;
using Ordering.Domain.Common;

namespace Ordering.Application.Contracts
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(int offset, int limit, Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] includeStrings);

        Task<T> AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);

        /*
        -agregar
        -borrar
        -obtener todos
        -obtener todos con un filtro
        -obtener todos con un filtro y paginacion, ordenanmientoy Joins
        obtener uno con un filtro
        -obtener por id
        -actualizar
         */
    }
}
