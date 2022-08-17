using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class CreateCleaningObjectTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private CleaningObjectMapper _cleaningObjectMapper;
        private BaseClearCommand _deleteFromDb;
        private int _id;
        private string _token;
        public CreateCleaningObjectTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _cleaningObjectSteps = new CleaningObjectSteps();
            _cleaningObjectMapper = new CleaningObjectMapper();
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
        [TestCaseSource(typeof(AddCleaningObject_WhenModelIsCorrect_TestSource))]
        public void CreateCleaningObject_WhenModelIsCorrect_ShouldCreateObject(CleaningObjectRequestModel cleaningObjectRequest)
        {
            cleaningObjectRequest.ClientId = _id;
            int cleaningObjectId = _cleaningObjectSteps.AddCleaningObjectTest(cleaningObjectRequest, _token);
            CleaningObjectResponseModel expectedCleaningObjectResponseModel = 
                _cleaningObjectMapper.MappCleaningObjectRequestModelToCleaningObjectResponseModel(cleaningObjectRequest, _id, cleaningObjectId);
            _cleaningObjectSteps.GetCleaningObjectByIdTest(cleaningObjectId, _token, expectedCleaningObjectResponseModel);
            List<CleaningObjectResponseModel> expectedCleaningObjectResponseModelList = new List<CleaningObjectResponseModel>()
            {
                expectedCleaningObjectResponseModel,
            };
            _cleaningObjectSteps.GetAllCleaningObjectsByClientIdTest(_id, _token, expectedCleaningObjectResponseModelList);
        }
    }
}
