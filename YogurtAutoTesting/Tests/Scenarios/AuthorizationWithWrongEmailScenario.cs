using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.Scenarios
{
    public class AuthorizationWithWrongEmailScenario
    {
        public void AuthModel()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "sonyaSanchez@rambler.ru",
                Password = "sonsan123456"
            };
        }
        
    }
}
