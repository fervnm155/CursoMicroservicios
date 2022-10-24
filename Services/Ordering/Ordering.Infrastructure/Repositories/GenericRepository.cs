using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;

        public GenericRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await orderContext.AddAsync(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            orderContext.Set<T>().Remove(entity);
            await orderContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await orderContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
            => await orderContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(int offset, int limit, Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] includeStrings)
        {
            //consulta dinamica
            IQueryable<T> query = orderContext.Set<T>();
            //paginacion
            query=query.Skip(offset).Take(limit);
            //join
            query = includeStrings.Aggregate(query, (current, itemInclude) => current.Include(itemInclude));
            //filtro si es que hay
            if (predicate is not null) query = query.Where(predicate);
            //ordenamiento si es que hay
            if (orderBy is not null) return await orderBy(query).ToListAsync();
            //resultado
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            ////es como select top 1
            //var entity = await orderContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            ////es como el selct top 1 pero no soporta null
            //var entity1 = await orderContext.Set<T>().FirstAsync(x => x.Id == id);
            //// trae el primero pero truena si habia mas de un resultado es como top 2
            //var entity2 = await orderContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            //// trae el primero pero truena si habia mas de un resultado pero no soporta null
            //var entity3 = await orderContext.Set<T>().SingleAsync(x => x.Id == id);

            //busca pro primary key
            var entity = await orderContext.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            orderContext.Entry(entity).State=EntityState.Modified;
            await orderContext.SaveChangesAsync();
            return entity;
        }
    }
}
