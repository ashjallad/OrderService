/// <summary>
/// The Order Business Entity namespace
/// </summary>
namespace OrderBusinessEntity.Order
{
    /// <summary>
    /// This class represnts the <see cref="OrderEntryEntity" business entity/>
    /// </summary>
    public class OrderEntryEntity
    {
        /// <summary>
        /// Gets or sets the Id/>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Product.ProductEntity"/> Product/>
        /// </summary>
        public Product.ProductEntity Product { get; set; }

        /// <summary>
        /// Gets or sets the Price/>
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Quantity/>
        /// </summary>
        public int Quantity { get; set; }
    }
}
