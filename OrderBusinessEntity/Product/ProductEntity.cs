/// <summary>
/// The Order Business Entity namespace
/// </summary>
namespace OrderBusinessEntity.Product
{
    /// <summary>
    /// This class represnts the <see cref="ProductEntity" business entity/>
    /// </summary>
    public class ProductEntity
    {
        /// <summary>
        /// Gets or sets the Id/>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Price/>
        /// </summary>
        public decimal Price { get; set; }
    }
}
