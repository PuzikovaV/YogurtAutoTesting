using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class AuthorizeTest
    {
        [Test]
        public void UserAuthorize_WhenAccountDoesNotExist_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "sonyaSanchez@rambler.ru",
                Password = "sonsan123456"
            };
            AuthorizationSteps authorize = new AuthorizationSteps();
            authorize.Authorize(authModel);
        }

        [Test]
        public void UserAuthorize_WhenPasswordIsWrong_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik00@gmail.com",
                Password = "sonsan123456"
            };
            AuthorizationSteps authorize = new AuthorizationSteps();
            authorize.Authorize(authModel);

        }
    }
}
