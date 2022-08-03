using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class ClientRegister_WhenModelIsCorrect_TestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "thebestKostya666",
                ConfirmPassword = "thebestKostya666",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };
        }
    }
}
