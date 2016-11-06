using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace MakeYourPizza.Domain.Abstract
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);

        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);
    }
}
