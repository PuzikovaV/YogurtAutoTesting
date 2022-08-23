using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;

namespace YogurtAutoTesting.Tests
{
    public class CreateOrderTest
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
        private int _clientId;
        private int _cleanerId;
        private int _serviceId;
        private int _bundleId;
        private int _orderId;
        private int _cleaningObjectId;
        private string _adminToken;
        private string _clientToken;
        private string _cleanerToken;
        private ClientRequestModel _clientModel;
        private DateTime _regDate;
        public CreateOrderTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _serviceSteps = new ServiceSteps();
            _cleanerSteps = new CleanerSteps();
            _bundlesSteps = new BundlesSteps();
            _clientSteps = new ClientsSteps();
            _orderSteps = new OrdersSteps();
            _deleteFromDb = new BaseClearCommand();
            _clientModel = new ClientRequestModel();
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
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "Admin@gmail.com",
                Password = "qwerty12345",
            };
            _adminToken = _authorizationSteps.Authorize(authModel);
            ServicesRequestModel serviceModel = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 0.5
            };
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
            _servicesResponseModel.Add(_servicesMapper.MappServiceRequestModelToServiceResponseModel(serviceModel, _serviceId));
            BundlesRequestModel bundleModel = new BundlesRequestModel()
            {
                Name = "Ежедневная уборка",
                Type = 1,
                RoomType = 2,
                Price = 3000,
                Duration = 2,
                Measure = 2,
                ServicesIds = new List<int>()
            };
            List<ServicesResponseModel> bundleServices = new List<ServicesResponseModel>();
            _bundleId = _bundlesSteps.CreateBundleTest(bundleModel, _adminToken);
            _bundlesResponseModel.Add(_bundleMapper.MappBundleRequestModelToBundleResponseModel(bundleModel, bundleServices, _bundleId));
            CleanerRequestModel cleanerModel = new CleanerRequestModel()
            {
                FirstName = "Зина",
                LastName = "Корзина",
                BirthDate = new DateTime(1990, 08, 09),
                Email = "korzinka@zinka.ru",
                Password = "ZiZi1234",
                ConfirmPassword = "ZiZi1234",
                Passport = "1254789654",
                Phone = "89998887744",
                Schedule = 1,
                Districts = new List<int>() { 5, 6, 8 },
                ServicesIds = new List<int>() { }
            };
            _regDate = DateTime.Now.Date;
            _cleanerId = _cleanerSteps.CreateCleanerTest(cleanerModel);
            List<ServicesResponseModel> cleanerServices = new List<ServicesResponseModel>();
            _cleanerResponseModel.Add(_cleanerMapper.MappCleanerRequestModelToCleanerResponseModel(cleanerModel, _cleanerId, _regDate, cleanerServices));
            _clientModel = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };
            _clientId = _authorizationSteps.RegisterClient(_clientModel);
            _clientResponse = _clientMapper.MappClientRequestModelToClientResponseModel(_clientModel, _clientId, _regDate);
            AuthRequestModel authClientModel = new AuthRequestModel()
            {
                Email = _clientModel.Email,
                Password = _clientModel.Password
            };
            _clientToken = _authorizationSteps.Authorize(authClientModel);
            CleaningObjectRequestModel cleaningObjectModel = new CleaningObjectRequestModel()
            {
                NumberOfRooms = 3,
                NumberOfBathrooms = 2,
                Square = 70,
                NumberOfWindows = 6,
                NumberOfBalconies = 2,
                Address = "ул. Ленина д. 48, кв. 3",
                District = 5,
                ClientId = _clientId
            };
            _cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectModel, _clientToken);
            _cleaningObjectResponse = _cleaningObjectMapper.MappCleaningObjectRequestModelToCleaningObjectResponseModel(cleaningObjectModel, _clientId, _cleaningObjectId);
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void CreateOrder_WhenModelIsCorrect_ShouldCreateModel()
        {
            OrderRequestModel orderModel = new OrderRequestModel()
            {
                ClientId = _clientId,
                CleaningObjectId = _cleaningObjectId,
                ServicesIds = new List<int> { _serviceId },
                BundlesIds = new List<int> { _bundleId },
                StartTime = new DateTime(2022, 10, 11, 13, 30, 00)
            };
            _orderId = _orderSteps.CreateOrderTest(orderModel, _clientToken);
            double price = 3300.00;
            DateTime endTime = new DateTime(2022, 10, 11, 16, 00, 00);
            DateTime updateTime = _orderSteps.GetUpdateTimeOrder(_orderId, _adminToken);
            int status = 1;
            OrdersResponseModel expectedModel = _orderMapper.MappOrderRequestModelToOrderResponseModel(orderModel, _orderId, _clientResponse, price, endTime, updateTime, status,
                _cleaningObjectResponse, _servicesResponseModel, _bundlesResponseModel, _cleanerResponseModel);
            _orderSteps.GetOrderByIdTest(_orderId, _clientToken, expectedModel);
            List<OrdersResponseModel> expectedList = new List<OrdersResponseModel>()
            {
                expectedModel
            };
            _orderSteps.GetAllOrdersTest(_adminToken, expectedList);

            List<ServicesResponseModel> expectedServicesList = _servicesResponseModel;
            _orderSteps.GetOrdersServicesByIdTest(_orderId, _adminToken, expectedServicesList);

            CleaningObjectResponseModel expectedCleaningObject = _cleaningObjectResponse;
            _orderSteps.GetOrdersCleaningObjectById(_orderId, _adminToken, expectedCleaningObject);

            _cleanerSteps.GetAllCleanersOrdersByIdTest(_cleanerId, _cleanerToken, expectedList);
            _clientSteps.GetAllClientOrdersByIdTest(_clientId, _clientToken, expectedList);
        }
    }
}
