using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class CreateOrder_WhenModelIsCorrect_TestCaseSource : IEnumerable
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
                    RoomType = 2,
                    Duration = 15
                },
                new BundlesRequestModel()
                {
                    Name = "Ежедневная уборка",
                    Type = 1,
                    RoomType = 2,
                    Price = 3000,
                    Duration = 120,
                    Measure = 2,
                    ServicesIds = new List<int>()
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
                },
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
                new AuthRequestModel()
                {
                    Email = "kostik@gmail.com",
                    Password = "thebestKostya666",
                },
                new CleaningObjectRequestModel()
                {
                    NumberOfRooms = 3,
                    NumberOfBathrooms = 2,
                    Square = 70,
                    NumberOfWindows = 6,
                    NumberOfBalconies = 2,
                    Address = "ул. Ленина д. 48, кв. 3",
                    District = 5
                }
            };
        }
    }
}
