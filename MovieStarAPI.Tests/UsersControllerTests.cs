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
using Newtonsoft.Json;

namespace MovieStarAPI.Tests
{
    public class UsersControllerTests
    {
        private const string RequestUriSignIN = "/users?action=signin";
        private const string RequestUriSignUP = "/users?action=signup";
        private const string MediaType = "application/json";
        Encoding uTF8 = Encoding.UTF8;
        private readonly ITestOutputHelper output;

        public UsersControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        // sign IN
        
        [Fact]
        public async Task sign_IN_existing_user_with_valid_credentials_receives_200_OK()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            StringContent stringContent = new StringContent(@"{""username"": ""carl@email.dk"", ""password"": ""carl1234"" }", uTF8, MediaType);

            HttpResponseMessage? responseMsg = await client.PostAsync(RequestUriSignIN, stringContent);
            //output.WriteLine("Statuscode:" + responseMsg.StatusCode);
            //output.WriteLine(stringContent.ReadAsStringAsync().Result);

            responseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task sign_IN_non_existing_user_receives_401_Unauthorized()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            StringContent stringContent = new StringContent(@"{""username"": ""noone@email.dk"", ""password"": ""carl1234"" }", uTF8, MediaType);
            HttpResponseMessage? responseMsg = await client.PostAsync(RequestUriSignIN, stringContent);

            responseMsg.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task sign_IN_existing_user_with_wrong_password_receives_401_Unauthorized()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            StringContent stringContent = new StringContent(@"{""username"": ""carl@email.dk"", ""password"": ""wrongpassword"" }", uTF8, MediaType);
            HttpResponseMessage? responseMsg = await client.PostAsync(RequestUriSignIN, stringContent);

            responseMsg.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        // sign UP

        /*  When running these tests, remember to remove any unwanted data from the database afterwards,
            and remember to update userName + displayName */

        //[Fact]
        public async Task sign_UP_non_existing_user_receives_201_Created()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            StringContent stringContent = new StringContent(@"{""username"": ""integrationtest2@email.dk"", ""password"": ""carl1234"", ""displayname"": ""Integrationtest2"" }", uTF8, MediaType);
            HttpResponseMessage? responseMsg = await client.PostAsync(RequestUriSignUP, stringContent);

            responseMsg.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        //[Fact]
        public async Task sign_UP_existing_user_receives_409_Conflict()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            StringContent stringContent = new StringContent(@"{""username"": ""carl@email.dk"", ""password"": ""carl1234"", ""displayname"": ""Carl"" }", uTF8, MediaType);
            HttpResponseMessage? responseMsg = await client.PostAsync(RequestUriSignUP, stringContent);

            responseMsg.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

    }
}
