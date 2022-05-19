using MovieStarAPI.Persistence;
using System.Net;

namespace MovieStarAPI.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GET_existing_user_with_valid_credentials_receives_200_OK()
        {
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser("anders@email.dk", "anders1234");
            Assert.That(contentResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GET_non_existing_user_receives_401_Unauthorized()
        {
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser("noone@email.dk", "carl1234");
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task GET_existing_user_with_wrong_password_receives_401_Unauthorized()
        {
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser("carl@email.dk", "wrongpassword");
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }
    }
}