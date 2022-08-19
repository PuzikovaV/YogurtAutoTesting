using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Support;
using YogurtAutoTesting.Tests.StepDefinitions;
using YogurtAutoTesting.Tests.TestSources;

namespace YogurtAutoTesting.Tests
{
    public class AuthorizeTest
    {
        private AuthorizationSteps _authorizationSteps;
        private BaseClearCommand _deleteFromDb;
        public AuthorizeTest()
        {
            _authorizationSteps = new AuthorizationSteps();
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

        [TestCaseSource(typeof(ClientRegister_WhenModelIsCorrect_TestSource))]
        public void UserAuthorize_WhenPasswordIsWrong_ShouldNotLogIn(ClientRequestModel clientModel, AuthRequestModel authModel)
        {
            authModel.Password = "qqqwwweee";
            _authorizationSteps.DoNotAuthorizeTest(authModel);

        }
    }
}
