using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class AddCleaningObject_WhenModelIsCorrect_TestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
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
                    District = 2
                }

            };
        }
    }
}
