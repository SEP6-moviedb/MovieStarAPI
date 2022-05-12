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

namespace MovieStarAPI.Tests
{
    public class UserRatingsControllerTests
    {
        private readonly ITestOutputHelper output;

        public UserRatingsControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task GET_retrieves_user_ratings()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/userratings");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GET_retrieves_one_user_rating_for_one_movieid()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetAsync("/userratings?movieid=414906");
            var responseContentString = response.Content.ReadAsStringAsync().Result;
            output.WriteLine("This is output from {0}", responseContentString + "<-wwwwwwwwwwwqwqqq");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
