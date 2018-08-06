using NLog;
using OrderDataModel.Repository;
using System;
using System.Data.Entity.Validation;

namespace OrderDataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private readonly CommerceDBEntities _context;
        private IGenericRepository<Order> _orderRepository;
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public UnitOfWork()
        {
            _context = new CommerceDBEntities();
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(_context);
                }

                return _orderRepository;
            }
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValErr in ex.EntityValidationErrors)
                {
                    _logger.Error(string.Format("{0}: Entity: \"{1}\". State: \"{2}\". Validation Errors:", DateTime.Now, entityValErr.Entry.Entity.GetType().Name, entityValErr.Entry.State));
                    foreach (var valErr in entityValErr.ValidationErrors)
                    {
                        _logger.Error(string.Format("- Property: \"{0}\", Error: \"{1}\"", valErr.PropertyName, valErr.ErrorMessage));
                    }
                }

                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
