using System.Collections;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests.TestSources
{
    public class AddCleaningObject_WhenModelIsCorrect_TestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new CleaningObjectRequestModel()
            {
                NumberOfRooms = 3,
                NumberOfBathrooms = 2,
                Square = 70,
                NumberOfWindows = 6,
                NumberOfBalconies = 2,
                Address = "ул. Ленина д. 48, кв. 3",
                District = 2,
                ClientId = 0
            };
        }
    }
}
