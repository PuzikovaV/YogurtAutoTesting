using System.Collections;
using YogurtAutoTesting.Models.Request;


namespace YogurtAutoTesting.Tests.TestSources
{
    public class RegisterThreeClients_WhenModelIsCorrect_TestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new List<ClientRequestModel>()
            {
                new ClientRequestModel()
                {
                    FirstName = "Константин",
                    LastName = "Придуманный",
                    BirthDate = new DateTime(1966, 06, 16, 00, 00, 00),
                    Password = "thebestKostya666",
                    ConfirmPassword = "thebestKostya666",
                    Email = "kostik@gmail.com",
                    Phone = "89996662233"
                },
                new ClientRequestModel()
                {
                    FirstName = "Василина",
                    LastName = "Васина",
                    BirthDate = new DateTime(1978, 04, 12, 00, 00, 00),
                    Password = "vasyok123456",
                    ConfirmPassword = "vasyok123456",
                    Email = "vasyok@gmail.com",
                    Phone = "86665556633"
                },
                new ClientRequestModel()
                {
                    FirstName = "Карл",
                    LastName = "Кларов",
                    BirthDate = new DateTime(1990, 12, 24, 00, 00, 00),
                    Password = "karally888",
                    ConfirmPassword = "karally888",
                    Email = "klarov@ya.ru",
                    Phone = "87774445566"
                }
            };
        }
    }
}

