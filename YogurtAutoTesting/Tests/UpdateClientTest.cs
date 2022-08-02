

using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Tests.StepDefinitions;

namespace YogurtAutoTesting.Tests
{
    public class UpdateClientTest
    {
        AuthorizationSteps _authorizationSteps;
        ClientsSteps _clientsSteps;

        public UpdateClientTest()
        {
            _authorizationSteps = new AuthorizationSteps();
            _clientsSteps = new ClientsSteps();
        }
        public void UpdateClient_WhenModelIsCorrect_ShouldUpdateClient()
        {
            ClientRequestModel model = new ClientRequestModel()
            {

            }
        }
    }
}
