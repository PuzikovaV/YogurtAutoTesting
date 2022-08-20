using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.TestSources;

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
        private int _clientId;
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
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);
            BundlesRequestModel bundleModel = new BundlesRequestModel()
            {
                Name = "Ежедневная уборка",
                Type = 1,
                RoomType = 2,
                Price = 3000,
                Duration = 120,
                Measure = 2,
                ServicesIds = new List<int>() { _serviceId }
            };
            _bundleId = _bundlesSteps.CreateBundleTest(bundleModel, _adminToken);
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
                ServicesIds = new List<int>(_serviceId)
            };
            _regDate = DateTime.Now.Date;
            _cleanerSteps.CreateCleanerTest(cleanerModel);
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
                CleaningObjectId = _cleaningObjectId,
                ServicesIds = new List<int> { _serviceId },
                BundlesIds = new List<int> { _bundleId },
                StartTime = new DateTime(2022, 10, 11)
            };
            _orderId = _orderSteps.CreateOrderTest(orderModel, _clientToken);
            OrdersResponseModel expectedModel = new OrdersResponseModel()
            {
                Id = _orderId,
                Client = new ClientResponseModel()
                {
                    Id = _clientId,
                    FirstName = _clientModel.FirstName,
                    LastName = _clientModel.LastName,
                    BirthDate = _clientModel.BirthDate,
                    RegistrationDate = _regDate,
                    Email = _clientModel.Email,
                    Phone = _clientModel.Phone
                },
            };
        }
    }
}
