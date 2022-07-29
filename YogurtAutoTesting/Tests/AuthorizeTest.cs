using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.Scenarios;
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
            AuthorizationStep authorize = new AuthorizationStep();
            authorize.Authorize(authModel, HttpStatusCode.NotFound);
        }

        [Test]
        public void UserAuthorize_WhenPasswordIsWrong_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik00@gmail.com",
                Password = "sonsan123456"
            };
            AuthorizationStep authorize = new AuthorizationStep();
            authorize.Authorize(authModel, HttpStatusCode.NotFound);

        }
    }
}
