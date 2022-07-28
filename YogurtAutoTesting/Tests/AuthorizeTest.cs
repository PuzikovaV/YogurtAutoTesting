using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests
{
    public class AuthorizeTest
    {
        private AuthClient _authClient = new AuthClient();
        [Test]
        public void UserAuthorize_WhenAccountDoesNotExist_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "sonyaSanchez@rambler.ru",
                Password = "sonsan123456"
            };
            HttpStatusCode excpectedCode = HttpStatusCode.NotFound;

            HttpResponseMessage authResponse = _authClient.Authorize(authModel);
            HttpStatusCode actualCode = authResponse.StatusCode;

            Assert.AreEqual(excpectedCode,actualCode);
        }

        [Test]
        public void UserAuthorize_WhenPasswordIsWrong_ShouldNotLogIn()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik00@gmail.com",
                Password = "sonsan123456"
            };
            HttpStatusCode excpectedCode = HttpStatusCode.NotFound;

            HttpResponseMessage authResponse = _authClient.Authorize(authModel);
            HttpStatusCode actualCode = authResponse.StatusCode;

            Assert.AreEqual(excpectedCode, actualCode);
        }
    }
}
