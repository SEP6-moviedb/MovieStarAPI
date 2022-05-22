using MovieStarAPI.Models;
using MovieStarAPI.Persistence;

namespace MovieStarAPI.Tests
{
    public class UserServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }


        // sign IN 

        [Test]
        public async Task sign_IN_existing_user_with_valid_credentials_receives_200_OK()
        {
            User user = new User { userName = "anders@email.dk", password = "anders1234" };
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task sign_IN_non_existing_user_receives_401_Unauthorized()
        {
            User user = new User { userName = "noone@email.dk", password = "carl1234" };
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task sign_IN_existing_user_with_wrong_password_receives_401_Unauthorized()
        {
            User user = new User { userName = "carl@email.dk", password = "wrongpassword" };
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.GetUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(401));
        }


        // sign UP 

        /*  When running these tests, remember to remove any unwanted data from the database afterwards,
            and remember to update userName + displayName */

        //[Test]
        public async Task sign_UP_non_existing_user_receives_201_Created()
        {
            User user = new User { userName = "unittest1@email.dk", password = "carl1234", displayName = "UnitTest1" };
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.PostUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(201));
        }

        //[Test]
        public async Task sign_UP_existing_user_receives_409_Conflict()
        {
            User user = new User { userName = "carl@email.dk", password = "carl1234", displayName = "Carl" };
            Microsoft.AspNetCore.Mvc.ContentResult? contentResult = await UserService.PostUser(user);
            Assert.That(contentResult.StatusCode, Is.EqualTo(409));
        }

    }
}