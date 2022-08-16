using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class UpdateCleaningObjectTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private BaseClearCommand _deleteFromDb;
        string _token;
        int _clientId;
        int _cleaningObjectId;

        public UpdateCleaningObjectTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _deleteFromDb = new BaseClearCommand();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _deleteFromDb.ClearBase();

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

            _clientId = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            _token = _authorizationSteps.Authorize(authModel);

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
            cleaningObjectRequest.ClientId = _clientId;
            _cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, _token);
        }
        [TearDown]
        public void TearDown()
        {
            _deleteFromDb.ClearBase();
        }
        [Test]
        public void UpdateCleaningObject_WhenModelIsCorrect_ShouldUpdateCleaningObject()
        {
            UpdateCleaningObjectRequestModel updateModel = new UpdateCleaningObjectRequestModel()
            {
                NumberOfRooms = 6,
                NumberOfBathrooms = 3,
                NumberOfBalconies = 4,
                NumberOfWindows = 9,
                Square = 150,
                Address = "ул. Ленина, д. 48, кв. 13",
                District = 5,
            };
            _cleaningObjectSteps.UpdateCleaningObject(updateModel, _cleaningObjectId, _token);

            CleaningObjectResponseModel expectedResponse = new CleaningObjectResponseModel()
            {

                Id = _cleaningObjectId,
                District = updateModel.District,
                NumberOfRooms = updateModel.NumberOfRooms,
                NumberOfBathrooms = updateModel.NumberOfBathrooms,
                Square = updateModel.Square,
                NumberOfWindows = updateModel.NumberOfWindows,
                NumberOfBalconies = updateModel.NumberOfBalconies,
                Address = updateModel.Address,
                ClientId = _clientId, 
            };

            _cleaningObjectSteps.GetCleaningObjectByIdTest(_cleaningObjectId, _token, expectedResponse);
        }
    }
}
