using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class RegisterCleaner_WhenServiceIsEmpty_TestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new CleanerRequestModel()
                {
                    FirstName = "Зина",
                    LastName = "Корзина",
                    BirthDate = new DateTime(1990, 08, 09),
                    Email = "korzinka@zinka.ru",
                    Password = "ZiZi1234",
                    ConfirmPassword = "ZiZi1234",
                    Passport = "1254789654",
                    Phone = "89998887744",
                    Schedule = 1,
                    Districts = new List<int>() { 5, 6, 8 },
                    ServicesIds = new List<int>()
                },
                new AuthRequestModel()
                {
                    Email = "korzinka@zinka.ru",
                    Password = "ZiZi1234",
                },
            };
        }
    }
}
