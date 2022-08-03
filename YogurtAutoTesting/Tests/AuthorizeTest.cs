using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class AuthorizeTest
    {
        private AuthorizationSteps _authorizationSteps;
        public AuthorizeTest()
        {
            _authorizationSteps = new AuthorizationSteps();
        }

        [Test]
        public void UserAuthorize_WhenAccountDoesNotExist_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "sonyaSanchez@rambler.ru",
                Password = "sonsan123456"
            };
            _authorizationSteps.DoNotAuthorizeTest(authModel);
        }

        [Test]
        public void UserAuthorize_WhenPasswordIsWrong_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik00@gmail.com",
                Password = "sonsan123456"
            };
            _authorizationSteps.DoNotAuthorizeTest(authModel);

        }
    }
}
