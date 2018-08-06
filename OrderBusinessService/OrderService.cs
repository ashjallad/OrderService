using System;
using System.Collections.Generic;
using OrderBusinessEntity.Order;
using System.Linq;
using OrderDataModel.UnitOfWork;
using OrderDataModel;
using AutoMapper;
using OrderBusinessService.MapperSettings;
using System.Transactions;

/// <summary>
/// The Order Server namespace
/// </summary>
namespace OrderBusinessService
{
    /// <summary>
    /// This class implements the <see cref="IOrderService" Order Service/>
    /// </summary>
    public class OrderService : IOrderService, IDisposable
    {
        private bool disposed = false;
        private readonly UnitOfWork _unitOfWork;

        public OrderService()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Creates a new <see cref="OrderEntity"/> Order
        /// </summary>
        /// <param name="orderEntity"> The <see cref="OrderEntity" Order entity/> to create</param>
        public void CreateOrder(OrderEntity orderEntity)
        {
            using (var tranScope = new TransactionScope(TransactionScopeOption.Required))
            {
                var order = Mapper.Map<OrderEntity, Order>(orderEntity);
                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.SaveChanges();
                tranScope.Complete();
            }
        }

        /// <summary>
        /// Gets <see cref="OrderEntity"/> Order by Id
        /// </summary>
        /// <param name="orderId">The <see cref="OrderEntity" Order/> Id</param>
        /// <returns>The <see cref="OrderEntity"/> Order if it exists, otherwise return null</returns>
        public OrderEntity GetOrderById(int orderId)
        {
            var order = _unitOfWork.OrderRepository.GetByID(orderId);
            if (order != null)
            {
                return Mapper.Map<Order, OrderEntity>(order);
            }

            return null;
        }

        /// <summary>
        /// Gets <see cref="OrderEntity"/> Order by Id
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <param name="orderId">The <see cref="OrderEntity" Order/> Id</param>
        /// <returns>The <see cref="OrderEntity"/> Order for a specific user. If does not exisit return null</returns>
        public OrderEntity GetOrderById(int customerId, int orderId)
        {
            var order = _unitOfWork.OrderRepository.Get(o => o.CustomerId.Equals(customerId) && o.Id.Equals(orderId)).FirstOrDefault();
            if (order != null)
            {
                return Mapper.Map<Order, OrderEntity>(order);
            }

            return null;
        }

        /// <summary>
        /// Gets a queryable list of <see cref="OrderEntity" Orders/ > by date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> within a date range.</returns>
        public IQueryable<OrderEntity> GetOrders(DateTime startDate, DateTime endDate)
        {
            var orders = _unitOfWork.OrderRepository.Get(o => o.CreateDate.Date >= startDate.Date && o.CreateDate.Date <= endDate.Date).ToList();
            if (orders.Any())
            {
                return Mapper.Map<List<Order>, List<OrderEntity>>(orders).AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of <see cref="OrderEntity" Orders/ > by date range for a specific user
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> within a date range for a specific customer.</returns>
        public IQueryable<OrderEntity> GetOrders(int customerId, DateTime startDate, DateTime endDate)
        {
            var orders = _unitOfWork.OrderRepository.Get(o => o.CustomerId.Equals(customerId) && o.CreateDate.Date >= startDate.Date && o.CreateDate.Date <= endDate.Date).ToList();
            if (orders.Any())
            {
                return Mapper.Map<List<Order>, List<OrderEntity>>(orders).AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Returns all orders for a specific user
        /// </summary>
        /// <returns>Gets <see cref="OrderEntity Orders"/>. if does not exist return null</returns>
        public IQueryable<OrderEntity> GetAllOrders()
        {
            var orders = _unitOfWork.OrderRepository.GetAll().ToList();
            if (orders.Any())
            {
                return Mapper.Map<List<Order>, List<OrderEntity>>(orders).AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Returns all orders for a specific user
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> for a specific customer. if does not exist return null</returns>
        public IQueryable<OrderEntity> GetAllOrders(int customerId)
        {
            var orders = _unitOfWork.OrderRepository.Get(o => o.CustomerId.Equals(customerId)).ToList();
            if (orders.Any())
            {
                return Mapper.Map<List<Order>, List<OrderEntity>>(orders).AsQueryable();
            }

            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
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
