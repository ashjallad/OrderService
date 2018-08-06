using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderBusinessService;
using OrderBusinessEntity.Order;
using OrderBusinessEntity.Product;
using System.Collections.Generic;

namespace OrderBusinessServiceTest
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestInitialize]
        public void InitializeTestData()
        {
        }

        [TestMethod]
        public void GetOrderByIdTest()
        {
            IOrderService _orderService = new OrderService();
            OrderEntity _order = _orderService.GetOrderById(4);
            Assert.IsNotNull(_order);
            Assert.IsTrue(_order.OrderEntries.Count == 2);
        }

        [TestMethod]
        public void GetAllOrdersTest()
        {
            IOrderService _orderService = new OrderService();
            IEnumerable<OrderEntity> _orders = _orderService.GetAllOrders();
            Assert.IsNotNull(_orders);
        }

        [TestMethod]
        public void GetOrdersByDateTest()
        {
            IOrderService _orderService = new OrderService();
            IEnumerable<OrderEntity> _orders = _orderService.GetOrders(new DateTime(2017,7,26), new DateTime(2017,7,29));
            Assert.IsNotNull(_orders);
        }

        [TestMethod]
        public void CreateOrderTest()
        {
            IOrderService _orderService = new OrderService();
            var _product1 = new ProductEntity() { Id = 2 };
            var _product2 = new ProductEntity() { Id = 2 };

            var _orderEntry1 = new OrderEntryEntity() { ProductId = 1, Quantity = 1, Price = 20.55M };
            var _orderEntry2 = new OrderEntryEntity() { ProductId = 2, Quantity = 4, Price = 22M };
            var _orderEntries = new List<OrderEntryEntity>();
            _orderEntries.Add(_orderEntry1);
            _orderEntries.Add(_orderEntry2);

            OrderEntity order = new OrderEntity() { CreateDate = new DateTime(), CustomerId = 4, OrderEntries = _orderEntries};
            _orderService.CreateOrder(order);
        }

        [TestCleanup]
        public void CleanTestData()
        {

        }
    }
}
