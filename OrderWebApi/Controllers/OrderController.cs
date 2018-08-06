using OrderBusinessEntity.Order;
using OrderBusinessService;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace OrderWebApi.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        /// <param name="OrderService">The <see cref="IOrderService"/> Order Service</param>
        public OrderController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }

        [HttpGet]
        [Route("api/order/get/{id}")]
        // Gets the order by id
        // Usage: GET api/order/1
        public IHttpActionResult GetOrderById(int id)
        {
            var _order = _orderService.GetOrderById(id);
            if (_order != null)
            {
                return Ok<OrderEntity>(_order);
            }

            return Content(HttpStatusCode.NotFound, String.Format ("Order with Id {0} not found", id));
        }

        [HttpGet]
        [Route("api/order/get/{customerId}/{id}")]
        // Gets the order by id for a specific customer
        // Usage: GET api/order/1/1
        public IHttpActionResult GetOrderByIdForCustomer(int customerId, int id)
        {
            var _order = _orderService.GetOrderById(customerId, id);
            if (_order != null)
            {
                return Ok<OrderEntity>(_order);
            }

            return Content(HttpStatusCode.NotFound, String.Format("Order with Id {0} not found for customer Id{1}", id, customerId));
        }

        [HttpGet]
        [Route("api/order/getall")]
        // Gets all orders
        // Usage: GET api/order/getall
        public IHttpActionResult GetAll()
        {
            var _orders = _orderService.GetAllOrders();
            if (_orders != null)
            {
                return Ok<IQueryable<OrderEntity>>(_orders);
            }

            return Content(HttpStatusCode.NotFound, "No orders found");
        }

        [HttpGet]
        [Route("api/order/getall/{customerId}")]
        // Gets all orders for a specific customer
        // Usage: GET api/order/getall/1
        public IHttpActionResult GetAllForCustomer(int customerId)
        {
            var _orders = _orderService.GetAllOrders(customerId);
            if (_orders != null)
            {
                return Ok<IQueryable<OrderEntity>>(_orders);
            }

            return Content(HttpStatusCode.NotFound, String.Format("No orders found for customer {0}", customerId));
        }

        [HttpGet]
        [Route("api/order/daterange/{startDate}/{endDate}")]
        // Gets the order by date range
        // Usage: GET api/order/daterange/2017-07-22/2017-07-29
        public IHttpActionResult GetOrderByDate(string startDate, string endDate)
        {
            try
            {
                var _startDate = BuildDateTimeFromYAFormat(startDate);
                var _endDate = BuildDateTimeFromYAFormat(endDate);
                var _orders = _orderService.GetOrders(_startDate, _endDate);
                if (_orders != null)
                {
                    return Ok<IQueryable<OrderEntity>>(_orders);
                }

                return Content(HttpStatusCode.NotFound, String.Format("No orders found between {0} and {1}", startDate.ToString(), endDate.ToString()));
            }
            catch (FormatException)
            {
                return Content(HttpStatusCode.RequestedRangeNotSatisfiable, "startDate/endDate date format is invalid. Should be yyyy-MM-dd");
            }
        }

        [HttpGet]
        [Route("api/order/daterange/{customerId}/{startDate}/{endDate}")]
        // Gets the order by date range for a specific customer
        // Usage: GET api/order/daterange/1/2017-07-22/2017-07-29
        public IHttpActionResult GetOrderByDateForCustomer(int customerId, string startDate, string endDate)
        {
            try
            {
                var _startDate = BuildDateTimeFromYAFormat(startDate);
                var _endDate = BuildDateTimeFromYAFormat(endDate);
                var _orders = _orderService.GetOrders(customerId, _startDate, _endDate);
                if (_orders != null)
                {
                    return Ok<IQueryable<OrderEntity>>(_orders);
                }

                return Content(HttpStatusCode.NotFound, String.Format("No orders found between {0} and {1}", startDate.ToString(), endDate.ToString()));
            }
            catch (FormatException)
            {
                return Content(HttpStatusCode.RequestedRangeNotSatisfiable, "startDate/endDate date format is invalid. Should be yyyy-MM-dd");
            }
        }

        [HttpPost]
        [Route("api/order")]
        // Usage: POST api/order
        public void Post([FromBody] OrderEntity orderEntity)
        {
            _orderService.CreateOrder(orderEntity);
        }

        private DateTime BuildDateTimeFromYAFormat(string dateString)
        {
            Regex regX = new Regex(@"^\d{4}-\d{2}-\d{2}");
            if (!regX.IsMatch(dateString))
            {
                 throw new FormatException();
            }

            DateTime dateTime = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            return dateTime;
        }
    }
}
