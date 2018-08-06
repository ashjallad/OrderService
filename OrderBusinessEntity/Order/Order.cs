using System.Collections.Generic;

/// <summary>
/// The Order Business Entity namespace
/// </summary>
namespace OrderBusinessEntity.Order
{
    /// <summary>
    /// This class represnts the <see cref="OrderEntity" business entity/>
    /// </summary>
    public class OrderEntity
    {
        /// <summary>
        /// Gets or sets the Id/>
        /// </summary>
        public int Id{ get; set; }

        /// <summary>
        /// Gets or sets a list of <see cref="OrderEntryEntity" Order Entry/>
        /// </summary>
        public List<OrderEntryEntity> OrderEntries { get; set; }

        /// <summary>
        /// Gets or sets the Total Price/>
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the order create date
        /// </summary>
        public System.DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the customer Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Customer.CustomerEntity" Customer/>
        /// </summary>
        public Customer.CustomerEntity Customer { get; set; }
    }
}
