using OrderBusinessEntity.Order;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The Order service interface namespace
/// </summary>
namespace OrderBusinessService
{
    /// <summary>
    /// The order service contract
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new <see cref="OrderEntity"/> Order
        /// </summary>
        void CreateOrder(OrderEntity order);

        /// <summary>
        /// Gets <see cref="OrderEntity"/> Order by Id
        /// </summary>
        /// <param name="orderId">The <see cref="OrderEntity" Order/> Id</param>
        /// <returns>The <see cref="OrderEntity"/> Order if it exists, otherwise return null</returns>
        OrderEntity GetOrderById(int orderId);

        /// <summary>
        /// Gets <see cref="OrderEntity"/> Order by Id
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <param name="orderId">The <see cref="OrderEntity" Order/> Id</param>
        /// <returns>The <see cref="OrderEntity"/> Order for a specific user. If does not exisit return null</returns>
        OrderEntity GetOrderById(int customerId, int orderId);

        /// <summary>
        /// Gets a queryable list of <see cref="OrderEntity" Orders/ > by date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> within a date range.</returns>
        IQueryable<OrderEntity> GetOrders(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets a list of <see cref="OrderEntity" Orders/ > by date range for a specific user
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> within a date range for a specific customer.</returns>
        IQueryable<OrderEntity> GetOrders(int customerId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Returns all orders for a specific user
        /// </summary>
        /// <returns>Gets <see cref="OrderEntity Orders"/>. if does not exist return null</returns>
        IQueryable<OrderEntity> GetAllOrders();

        /// <summary>
        /// Returns all orders for a specific user
        /// </summary>
        /// <param name="customerId">The <see cref="CustomerEntity" Customer Entity/> Id</param>
        /// <returns>Gets <see cref="OrderEntity Orders"/> for a specific customer. if does not exist return null</returns>
        IQueryable<OrderEntity> GetAllOrders(int customerId);
    }
}
