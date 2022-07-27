using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YogurtAutoTesting.HttpClients;
using YogurtAutoTesting.Models.Request;

namespace YogurtAutoTesting.Tests
{
    public class CreateCleaningObjectTest
    {
        private CleaningObjectClient _cleaningObject = new CleaningObjectClient();
        private AuthClient _authClient = new AuthClient();
        [Test]
        public void CreateCleaningObject_WhenModelIsCorrect_ShouldCreateObject()
        {
            AuthRequestModel authModel = new AuthRequestModel()
            {
                Email = "kostik0@gmail.com",
                Password = "thebestKostya666",
            };
            HttpStatusCode expectedAuthCode = HttpStatusCode.OK;

            HttpResponseMessage authResponse = _authClient.Authorize(authModel);
            HttpStatusCode actualAuthCode = authResponse.StatusCode;
            string actualToken = authResponse.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(expectedAuthCode, actualAuthCode);
            Assert.NotNull(actualToken);

            string token = actualToken;

            CleaningObjectRequestModel cleaningObjectRequest = new CleaningObjectRequestModel()
            {
                NumberOfRooms = 3,
                NumberOfBathrooms = 2,
                Square = 70,
                NumberOfWindows = 6,
                NumberOfBalconies = 2,
                Address = "ул. Ленина д. 48, кв. 3",
                ClientId = 41
            };

            string id = _cleaningObject.CreateACleaningObject(cleaningObjectRequest, HttpStatusCode.Created, token).ReadAsStringAsync().Result;
            int? actualObjectId = Convert.ToInt32(id);

            Assert.NotNull(actualObjectId);
            Assert.IsTrue(actualObjectId>0);
        }
    }
}
