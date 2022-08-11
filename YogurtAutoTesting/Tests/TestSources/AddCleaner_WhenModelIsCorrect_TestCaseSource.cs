using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class AddCleaner_WhenModelIsCorrect_TestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthRequestModel()
                {
                    Email = "Admin@gmail.com",
                    Password = "qwerty12345",
                },
                new ServicesRequestModel()
                {
                    Name = "Помыть микроволновку",
                    Price = 300.00,
                    Unit = "Кухня",
                    Duration = 15
                },
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
                    Districts = new List<int>() { 5, 6, 8 }
                }
            };
        }
    }
}
