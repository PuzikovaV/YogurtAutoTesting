using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class ClientRegister_WhenPasswordIsSevenSymbols_TestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new ClientRequestModel()
            {
                FirstName = "Константин",
                LastName = "Придуманный",
                BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                Password = "1234567",
                ConfirmPassword = "1234567",
                Email = "kostik@gmail.com",
                Phone = "89996662233"
            };

        }
    }
}
