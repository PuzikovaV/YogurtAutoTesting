using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class CreateCleaningObjectTest
    {
        AuthorizationSteps _authorizationSteps = new AuthorizationSteps();
        CleaningObjectSteps _cleaningObjectSteps = new CleaningObjectSteps();

        [Test]
        public void CreateCleaningObject_WhenModelIsCorrect_ShouldCreateObject()
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

            int id = _authorizationSteps.RegisterClient(clientRequest);

            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = clientRequest.Email,
                Password = clientRequest.Password,
            };

            string token = _authorizationSteps.Authorize(authModel);

            CleaningObjectRequestModel cleaningObjectRequest = new CleaningObjectRequestModel()
            {
                NumberOfRooms = 3,
                NumberOfBathrooms = 2,
                Square = 70,
                NumberOfWindows = 6,
                NumberOfBalconies = 2,
                Address = "ул. Ленина д. 48, кв. 3",
                ClientId = id
            };

            int cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, token);

            CleaningObjectResponseModel expectedCleaningObjectResponseModel = new CleaningObjectResponseModel()
            {
                Id = cleaningObjectId,
                NumberOfRooms = cleaningObjectRequest.NumberOfRooms,
                NumberOfBathrooms = cleaningObjectRequest.NumberOfBathrooms,
                Square = cleaningObjectRequest.Square,
                NumberOfWindows = cleaningObjectRequest.NumberOfWindows,
                NumberOfBalconies = cleaningObjectRequest.NumberOfBalconies,
                Address = cleaningObjectRequest.Address,
                ClientId = cleaningObjectRequest.ClientId,
            };

            List<CleaningObjectResponseModel> expectedCleaningObjectResponseModelList = new List<CleaningObjectResponseModel>
            {
                expectedCleaningObjectResponseModel,
            };
            _cleaningObjectSteps.GetAllCleaningObjectsByClientIdTest(cleaningObjectId, token, expectedCleaningObjectResponseModelList);
        }
    }
}
