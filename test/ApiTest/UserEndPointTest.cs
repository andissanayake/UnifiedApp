using FluentAssertions;
using Service;
using Service.UserGroup;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiTest
{
    public class UserEndPointTest(Fixture fixture) : IClassFixture<Fixture>
    {
        private readonly Fixture _fixture = fixture;

        [Fact]
        public async void LoginSuccessTest()
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

            }
            else
            {
                Assert.Fail("Api call failed.");
            }
        }

        [Fact]
        public async void LoginFailTest()
        {
            var userLoginRequest = new UserLoginRequest
            {
                Email = "UnifiedAppAdmin",
                Password = "wrong_password"
            };

            var jsonContent = JsonSerializer.Serialize(userLoginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _fixture.WebApplication.CreateClient().PostAsync("/User/Login", content);
            if (response.IsSuccessStatusCode)
            {
                response.Should().NotBeNull();
                var userLoginResponse = await response.Content.ReadFromJsonAsync<AppResponse<UserLoginResponse>>();
                userLoginResponse.Should().NotBeNull();
                userLoginResponse?.IsSucceed.Should().BeFalse();
                userLoginResponse?.Data.Should().BeNull();

            }
            else
            {
                Assert.Fail("Api call failed.");
            }
        }

    }
}