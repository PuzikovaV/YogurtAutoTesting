﻿using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Support.Mappers;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class DeleteCleaningObjectTest
    {
        private AuthorizationSteps _authorizationSteps;
        private CleaningObjectSteps _cleaningObjectSteps;
        private BaseClearCommand _deleteFromDb;
        private CleaningObjectMapper _cleaningObjectMapper;
        private string _token;
        private int _clientId;

        public DeleteCleaningObjectTest()
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

            _clientId = _authorizationSteps.RegisterClient(clientRequest);

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
        public void DeleteCleaningObject_WhenIdIsCorrect_ShouldDeleteCleaningObject(CleaningObjectRequestModel requestModel)
        {
            requestModel.ClientId = _clientId;
            int idObject = _cleaningObjectSteps.AddCleaningObjectTest(requestModel, _token);
            _cleaningObjectSteps.DeleteCleaningObjectTest(idObject, _token);
            List<CleaningObjectResponseModel> expectedModel = new List<CleaningObjectResponseModel>();
            _cleaningObjectSteps.GetAllCleaningObjectsByClientIdTest(_clientId, _token, expectedModel);
        }
    }
}
