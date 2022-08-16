using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class GetAllCleanersByAdminTest
    {
        private AuthorizationSteps _authorizeSteps;
        private CleanerSteps _cleanerSteps;
        private ServiceSteps _serviceSteps;
        private BaseClearCommand _deleteFromDb;
        public string _adminToken;
        public int _serviceId;
        private List<int> _cleanerIds;

        public GetAllCleanersByAdminTest()
        {
            _authorizeSteps = new AuthorizationSteps();
            _serviceSteps = new ServiceSteps();
            _cleanerSteps = new CleanerSteps();
            _deleteFromDb = new BaseClearCommand();
            _cleanerIds = new List<int>();
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
            _adminToken = _authorizeSteps.Authorize(authModel);

            ServicesRequestModel serviceModel = new ServicesRequestModel()
            {
                Name = "Помыть микроволновку",
                Price = 300.00,
                Unit = "Кухня",
                RoomType = 2,
                Duration = 15
            };
            _serviceId = _serviceSteps.CreateServiceTest(serviceModel, _adminToken);


        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void GetAllCleanersByAdmin_WhenModelIsCorrect_ShouldGetAllCleaners()
        {
            List<CleanerRequestModel> cleanerRequest = new List<CleanerRequestModel>()
            {
                new CleanerRequestModel()
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
                },
                new CleanerRequestModel()
                {
                    FirstName = "Лариса",
                    LastName = "Шмидт",
                    BirthDate = new DateTime(1985, 07, 10),
                    Email = "lara@ya.ru",
                    Password = "123456789",
                    ConfirmPassword = "123456789",
                    Passport = "4565888999",
                    Phone = "85556662211",
                    Schedule = 2,
                    Districts = new List<int>() { 10, 4, 6, 2 },
                    ServicesIds = new List<int>(_serviceId)
                }
            };
            DateTime regDate = DateTime.Now.Date;
            foreach(var cleaner in cleanerRequest)
            {
                int id = _cleanerSteps.CreateCleanerTest(cleaner);
                _cleanerIds.Add(id);
            };

            List<CleanerResponseModel> expectedCleaners = new List<CleanerResponseModel>();
            for (int i = 0; i < _cleanerIds.Count; i++)
            {
                expectedCleaners.Add(new CleanerResponseModel
                {
                    Id = _cleanerIds[i],
                    FirstName = cleanerRequest[i].FirstName,
                    LastName = cleanerRequest[i].LastName,
                    BirthDate = cleanerRequest[i].BirthDate,
                    Email = cleanerRequest[i].Email,
                    Phone = cleanerRequest[i].Phone,
                    RegistrationDate = regDate.Date
                });
            };
            _cleanerSteps.GetAllCleanersByAdminTest(_adminToken, expectedCleaners);

        }
    }
}
