using MovieStarAPI.Models;
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
            User user = new User();
            user.userName = "anders@email.dk";
            user.password = "anders1234";
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GET_non_existing_user_receives_401_Unauthorized()
        {
            User user = new User();
            user.userName = "noone@email.dk";
            user.password = "carl1234";
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task GET_existing_user_with_wrong_password_receives_401_Unauthorized()
        {
            User user = new User();
            user.userName = "carl@email.dk";
            user.password = "wrongpassword";
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }
    }
}