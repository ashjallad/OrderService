using NLog;
using System;
using System.Data.Entity;
using System.Linq;

namespace OrderDataModel.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity :class
    {
        private CommerceDBEntities _context;
        private DbSet<TEntity> _dbset;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public GenericRepository(CommerceDBEntities context)
        {
            this._context = context;
            this._dbset = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            try {
                _dbset.Add(entity);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, String.Format("Faild to Add/Insert the {0}", entity.GetType().ToString()));
                throw;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbset.Find(id);
        }

        public virtual IQueryable<TEntity> Get(Func<TEntity, bool> where)
        {
            return _dbset.Where(where).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbset.ToList().AsQueryable();
        }
    }
}
