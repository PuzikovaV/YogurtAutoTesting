using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;

namespace YogurtAutoTesting.Tests
{
    public class CreateCleaningObjectTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private BaseClearCommand _deleteFromDb;
        private int _id;
        private string _token;
        public CreateCleaningObjectTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _deleteFromDb = new BaseClearCommand();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            _deleteFromDb.ClearBase();
        }
        [SetUp]
        public void SetUp()
        {
            ClientRequestModel clientRequest = new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik08@gmail.com",
                Phone = "89996662233"
            };

            _id = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

           _token = _authorizationSteps.Authorize(authModel);
        }

        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void CreateCleaningObject_WhenModelIsCorrect_ShouldCreateObject()
        {
            CleaningObjectRequestModel cleaningObjectRequest = new CleaningObjectRequestModel()
            {
                NumberOfRooms = 3,
                NumberOfBathrooms = 2,
                Square = 70,
                NumberOfWindows = 6,
                NumberOfBalconies = 2,
                Address = "ул. Ленина д. 48, кв. 3",
                District = 2,  
            };
            cleaningObjectRequest.ClientId = _id;
            int cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, _token);

            CleaningObjectResponseModel expectedCleaningObjectResponseModel = new CleaningObjectResponseModel()
            {
                Id = cleaningObjectId,
                NumberOfRooms = cleaningObjectRequest.NumberOfRooms,
                NumberOfBathrooms = cleaningObjectRequest.NumberOfBathrooms,
                Square = cleaningObjectRequest.Square,
                NumberOfWindows = cleaningObjectRequest.NumberOfWindows,
                NumberOfBalconies = cleaningObjectRequest.NumberOfBalconies,
                Address = cleaningObjectRequest.Address,
                ClientId = _id,
                District = 2

            };
            _cleaningObjectSteps.GetCleaningObjectByIdTest(cleaningObjectId, _token, expectedCleaningObjectResponseModel);

            List<CleaningObjectResponseModel> expectedCleaningObjectResponseModelList = new List<CleaningObjectResponseModel>
            {
                expectedCleaningObjectResponseModel,
            };
            _cleaningObjectSteps.GetAllCleaningObjectsByClientIdTest(_id, _token, expectedCleaningObjectResponseModelList);
        }
    }
}
