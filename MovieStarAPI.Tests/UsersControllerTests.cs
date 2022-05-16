using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using Newtonsoft.Json.Linq;

namespace MovieStarAPI.Tests
{
    public class UsersControllerTests
    {
        private readonly ITestOutputHelper output;

        public UsersControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task GET_existing_user_with_valid_credentials_receives_200_OK()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/users?username=carl@email.dk&password=carl1234");
            string? responseContentString = response.Content.ReadAsStringAsync().Result;
            output.WriteLine("This is output from {0}", responseContentString);

            responseContentString.Should().Be("200");
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GET_non_existing_user_receives_401_Unauthorized()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/users?username=noone@email.dk&password=carl1234");
            string? responseContentString = response.Content.ReadAsStringAsync().Result;
            output.WriteLine("Output: "+ responseContentString);

            responseContentString.Should().Be("401");
            //response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GET_existing_user_with_wrong_password_receives_401_Unauthorized()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/users?username=carl@email.dk&password=wrongpassword");
            string? responseContentString = response.Content.ReadAsStringAsync().Result;
            output.WriteLine("Output: " + responseContentString);

            responseContentString.Should().Be("401");
            //response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
