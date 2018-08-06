using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderDataModel.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
         void Add(TEntity entity);

         TEntity GetByID(object id);

         IQueryable<TEntity> Get(Func<TEntity, bool> where);

        IQueryable<TEntity> GetAll();
    }
}