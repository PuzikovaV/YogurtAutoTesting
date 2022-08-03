using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class ClientRegister_WhenPasswordLessThenEightSymbols_TestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "the8",
                ConfirmPassword = "the8",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };
        }
    }
}
