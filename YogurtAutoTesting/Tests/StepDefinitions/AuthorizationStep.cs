using System.Net;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.StepDefinitions
{
    public class AuthorizationStep
    {
        public void Authorize(AuthRequestModel model, HttpStatusCode excpectedCode)
        {
            AuthClient authClient = new AuthClient();
            HttpResponseMessage authResponse = authClient.Authorize(model);
            HttpStatusCode actualCode = authResponse.StatusCode;

            Assert.AreEqual(excpectedCode, actualCode);
        }
        
    }
}
