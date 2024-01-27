using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Service.UserGroup;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiTest
{
    public class TokenTest(Fixture fixture) : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture = fixture;

        [Fact]
        public async void ApiSuccessTest()
        {
            var userLoginRequest = new UserLoginRequest
            {
                Email = "UnifiedAppAdmin",
                Password = "UnifiedAppAdmin1!"
            };

            var jsonContent = JsonSerializer.Serialize(userLoginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _fixture.WebApplication.CreateClient().PostAsync("/User/Login", content);
            if (response.IsSuccessStatusCode)
            {
                response.Should().NotBeNull();
                var userLoginResponse = await response.Content.ReadFromJsonAsync<AppResponse<UserLoginResponse>>();
                userLoginResponse.Should().NotBeNull();
                userLoginResponse?.IsSucceed.Should().BeTrue();
                userLoginResponse?.Data.Should().NotBeNull();
                userLoginResponse?.Data?.AccessToken.Should().NotBeNull();
                userLoginResponse?.Data?.RefreshToken.Should().NotBeNull();

                //call protected api
                using var newClient = _fixture.WebApplication.CreateClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/User/Profile");
                request.Headers.Add("Authorization", "Bearer " + userLoginResponse?.Data?.AccessToken);
                var apiRes = await newClient.SendAsync(request);
                apiRes.Should().NotBeNull();
                apiRes.StatusCode.Should().Be(HttpStatusCode.OK);

            }
            else
            {
                Assert.Fail("Api call failed.");
            }
        }

    }
}