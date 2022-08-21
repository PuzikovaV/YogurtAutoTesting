using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class DeleteOrderByIdTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private ServiceSteps _serviceSteps;
        private BundlesSteps _bundlesSteps;
        private OrdersSteps _orderSteps;
        private CleanerSteps _cleanerSteps;
        private BaseClearCommand _deleteFromDb;

        public DeleteOrderByIdTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _serviceSteps = new ServiceSteps();
            _cleanerSteps = new CleanerSteps();
            _bundlesSteps = new BundlesSteps();
            _orderSteps = new OrdersSteps();
            _deleteFromDb = new BaseClearCommand();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [TestCaseSource(typeof(CreateOrder_WhenModelIsCorrect_TestCaseSource))]
        public void DeleteOrderById_WhenIdIsCorrect_ShouldDeleteOrder(AuthRequestModel adminAuthModel, ServicesRequestModel servicesRequest, 
            BundlesRequestModel bundlesRequest, CleanerRequestModel cleanerRequest, ClientRequestModel clientModel, 
            AuthRequestModel clientAuthModel, CleaningObjectRequestModel cleaningObjectRequest)
        {
            DateTime regDate = DateTime.Now.Date;
            string adminToken = _authorizationSteps.Authorize(adminAuthModel);
            int serviceId = _serviceSteps.CreateServiceTest(servicesRequest, adminToken);
            int bundleId = _bundlesSteps.CreateBundleTest(bundlesRequest, adminToken);
            cleanerRequest.ServicesIds = new List<int>() { serviceId };
            _cleanerSteps.CreateCleanerTest(cleanerRequest);
            int clientId = _authorizationSteps.RegisterClient(clientModel);
            string clientToken = _authorizationSteps.Authorize(clientAuthModel);
            int cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, clientToken);
            OrderRequestModel orderRequest = new OrderRequestModel()
            {
                ClientId = clientId,
                CleaningObjectId = cleaningObjectId,
                BundlesIds = new List<int>() { bundleId },
                ServicesIds = new List<int>() { serviceId },
                StartTime = new DateTime (2022,09,09,13,00,00)
            };
            int orderId = _orderSteps.CreateOrderTest(orderRequest, clientToken);
            _orderSteps.DeleteOrderByIdTest(orderId, clientToken);
            List<OrdersResponseModel> expected = new List<OrdersResponseModel>();
            _orderSteps.GetAllOrdersTest(adminToken, expected);
        }
    }
}
