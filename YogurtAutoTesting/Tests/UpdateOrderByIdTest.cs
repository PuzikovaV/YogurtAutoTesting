using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class UpdateOrderByIdTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private ServiceSteps _serviceSteps;
        private ClientsSteps _clientSteps;
        private CleanerSteps _cleanerSteps;
        private BundlesSteps _bundlesSteps;
        private OrdersSteps _orderSteps;
        private BaseClearCommand _deleteFromDb;
        private ClientMapper _clientMapper;
        private CleaningObjectMapper _cleaningObjectMapper;
        private ServicesMapper _servicesMapper;
        private CleanerMapper _cleanerMapper;
        private ClientResponseModel _clientResponse;
        private BundleMapper _bundleMapper;
        private OrderMapper _orderMapper;
        private CleaningObjectResponseModel _cleaningObjectResponse;
        private List<BundlesResponseModel> _bundlesResponseModel;
        private List<ServicesResponseModel> _servicesResponseModel;
        private List<CleanerResponseModel> _cleanerResponseModel;
        private List<CommentsResponseModel> _commentsResponse;

        public UpdateOrderByIdTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _serviceSteps = new ServiceSteps();
            _cleanerSteps = new CleanerSteps();
            _bundlesSteps = new BundlesSteps();
            _clientSteps = new ClientsSteps();
            _orderSteps = new OrdersSteps();
            _deleteFromDb = new BaseClearCommand();
            _bundleMapper = new BundleMapper();
            _clientMapper = new ClientMapper();
            _servicesMapper = new ServicesMapper();
            _cleanerMapper = new CleanerMapper();
            _orderMapper = new OrderMapper();
            _cleaningObjectMapper = new CleaningObjectMapper();
            _cleanerResponseModel = new List<CleanerResponseModel>();
            _clientResponse = new ClientResponseModel();
            _cleaningObjectResponse = new CleaningObjectResponseModel();
            _cleanerResponseModel = new List<CleanerResponseModel>();
            _bundlesResponseModel = new List<BundlesResponseModel>();
            _servicesResponseModel = new List<ServicesResponseModel>();
            _commentsResponse = new List<CommentsResponseModel>();
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
            _servicesResponseModel.Add(_servicesMapper.MappServiceRequestModelToServiceResponseModel(servicesRequest, serviceId));

            int bundleId = _bundlesSteps.CreateBundleTest(bundlesRequest, adminToken);
            _bundlesResponseModel.Add(_bundleMapper.MappBundleRequestModelToBundleResponseModel(bundlesRequest, _servicesResponseModel, bundleId));

            cleanerRequest.ServicesIds = new List<int>() { serviceId };
            int cleanerId = _cleanerSteps.CreateCleanerTest(cleanerRequest);
            _cleanerResponseModel.Add(_cleanerMapper.MappCleanerRequestModelToCleanerResponseModel(cleanerRequest, cleanerId, regDate, _servicesResponseModel));

            int clientId = _authorizationSteps.RegisterClient(clientModel);
            string clientToken = _authorizationSteps.Authorize(clientAuthModel);
            _clientResponse = _clientMapper.MappClientRequestModelToClientResponseModel(clientModel, clientId, regDate);

            int cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, clientToken);
            _cleaningObjectResponse = _cleaningObjectMapper.MappCleaningObjectRequestModelToCleaningObjectResponseModel(cleaningObjectRequest, clientId, cleaningObjectId);

            OrderRequestModel orderRequest = new OrderRequestModel()
            {
                ClientId = clientId,
                CleaningObjectId = cleaningObjectId,
                BundlesIds = new List<int>() { bundleId },
                ServicesIds = new List<int>() { serviceId },
                StartTime = new DateTime(2022, 09, 09, 13, 00, 00)
            };
            int orderId = _orderSteps.CreateOrderTest(orderRequest, clientToken);

            UpdateOrderRequestModel updateRequest = new UpdateOrderRequestModel()
            {
                BundlesIds = new List<int>() { bundleId },
                CleanersBandIds = new List<int>() { cleanerId },
                ServicesIds = new List<int>() { serviceId },
                StartTime = new DateTime(2022, 12, 20, 14, 30, 00),
            };
            _orderSteps.UpdateOrderById(updateRequest, orderId, adminToken);
            DateTime updateTime = DateTime.Now.Date;
            int price = 500;
            DateTime endTime = DateTime.Now.Date;
            int status = 2;
            OrdersResponseModel expected = _orderMapper.MappUpdateOrderRequestModelToOrderResponseModel(updateRequest, orderId, _clientResponse,
                price, endTime, updateTime, status, _cleaningObjectResponse, _servicesResponseModel, _bundlesResponseModel,
                _cleanerResponseModel, _commentsResponse);
            _orderSteps.GetOrderByIdTest(orderId, adminToken, expected);

            int updateStatus = 3;
            _orderSteps.UpdateStatusByOrderIdTest(orderId, updateStatus, adminToken);
            OrdersResponseModel expectedStatus = _orderMapper.MappUpdateOrderRequestModelToOrderResponseModel(updateRequest, orderId, _clientResponse,
                price, endTime, updateTime, updateStatus, _cleaningObjectResponse, _servicesResponseModel, _bundlesResponseModel,
                _cleanerResponseModel, _commentsResponse);
            _orderSteps.GetOrderByIdTest(orderId, adminToken, expectedStatus);

            int paymentStatus = 2;
            _orderSteps.UpdatePaymentStatusByOrderIdTest(orderId, paymentStatus, adminToken);
        }
    }
}
