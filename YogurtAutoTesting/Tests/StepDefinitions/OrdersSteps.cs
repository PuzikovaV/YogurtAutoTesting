using System.Net;
using System.Text.Json;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class OrdersSteps
    {
        private OrdersClient _ordersClient;
        public OrdersSteps()
        {
            _ordersClient = new OrdersClient();
        }
        public int CreateOrderTest(OrderRequestModel model, string token)
        {
            HttpContent content = _ordersClient.CreateOrder(model, token, HttpStatusCode.Created);
            int id = Convert.ToInt32(content.ReadAsStringAsync().Result);
            Assert.IsTrue(id > 0);
            return id;
        }
        public DateTime GetUpdateTimeOrder(int id, string token)
        {
            HttpContent content = _ordersClient.GetOrderByOrderId(id, token, HttpStatusCode.OK);
            OrdersResponseModel actual = JsonSerializer.Deserialize<OrdersResponseModel>(content.ReadAsStringAsync().Result);
            return actual.UpdateTime;
        }
        public OrdersResponseModel GetOrderByIdTest(int id, string token, OrdersResponseModel expected)
        {
            HttpContent content = _ordersClient.GetOrderByOrderId(id, token, HttpStatusCode.OK);
            OrdersResponseModel actual = JsonSerializer.Deserialize<OrdersResponseModel>(content.ReadAsStringAsync().Result);
            Assert.AreEqual(expected, actual);
            return actual;
        }
        public List<OrdersResponseModel> GetAllOrdersTest(string token, List<OrdersResponseModel> expected)
        {
            HttpContent content = _ordersClient.GettAllOrders(token, HttpStatusCode.OK);
            List<OrdersResponseModel> actual = JsonSerializer.Deserialize<List<OrdersResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        public void DeleteOrderByIdTest(int id, string token)
        {
            _ordersClient.DeleteOrderById(id, token, HttpStatusCode.NoContent);
        }
        public void UpdateOrderById(UpdateOrderRequestModel model, int id, string token)
        {
            _ordersClient.UpdateOrderById(model, id, token, HttpStatusCode.NoContent);
        }
        public List<ServicesResponseModel> GetOrdersServicesByIdTest(int id, string token, List<ServicesResponseModel> expected)
        {
            HttpContent content = _ordersClient.GetServicesByOrderId(id, token, HttpStatusCode.OK);
            List<ServicesResponseModel> actual = JsonSerializer.Deserialize<List<ServicesResponseModel>>(content.ReadAsStringAsync().Result);
            CollectionAssert.AreEquivalent(expected, actual);
            return actual;
        }
        public CleaningObjectResponseModel GetOrdersCleaningObjectById(int id, string token, CleaningObjectResponseModel expected)
        {
            HttpContent content = _ordersClient.GetCleaningObjectByOrderId(id, token, HttpStatusCode.OK);
            CleaningObjectResponseModel actual = JsonSerializer.Deserialize<CleaningObjectResponseModel>(content.ReadAsStringAsync().Result);
            Assert.AreEqual(expected, actual);
            return actual;
        }
        public void UpdateStatusByOrderIdTest(int id, int status, string token)
        {
            _ordersClient.ChangeOrdersStatusById(id, status, token, HttpStatusCode.NoContent);
        }
        public void UpdatePaymentStatusByOrderIdTest(int id, int paymentStatus, string token)
        {
            _ordersClient.ChangeOrdersStatusById(id, paymentStatus, token, HttpStatusCode.NoContent);
        }
    }
}
