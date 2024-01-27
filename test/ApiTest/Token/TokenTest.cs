using Api;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Service.UserGroup;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ApiTest.Token
{

    public class TokenTest(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async void ApiSuccessTest()
        {
            var token = await GetToken();
            var apiRes = await GetSecureApiRes(token.AccessToken);
            apiRes.Should().NotBeNull();
            apiRes.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async void ApiFailureTest()
        {
            var apiRes = await GetSecureApiRes("");
            apiRes.Should().NotBeNull();
            apiRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void TokenExpireTest()
        {
            var token = await GetToken();

            await Task.Delay(12000);
            HttpRequestMessage request = new(HttpMethod.Post, "/User/Profile");
            request.Headers.Add("Authorization", "Bearer " + token.AccessToken);
            var apiRes = await _client.SendAsync(request);
            apiRes.Should().NotBeNull();
            apiRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void GetRefreshTokenTest()
        {
            var token = await GetToken();
            await Task.Delay(12000);
            var rtRes = await GetRefreshToken(new UserRefreshTokenRequest
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            });
            rtRes.Should().NotBeNull();
            rtRes.StatusCode.Should().Be(HttpStatusCode.OK);
            var userRefreshTokenResponse = await rtRes.Content.ReadFromJsonAsync<AppResponse<UserRefreshTokenResponse>>();
            userRefreshTokenResponse.Should().NotBeNull();
            userRefreshTokenResponse?.IsSucceed.Should().BeTrue();
            userRefreshTokenResponse?.Data?.Should().NotBeNull();
            var apiRes = await GetSecureApiRes(userRefreshTokenResponse?.Data?.AccessToken ?? "");
            apiRes.Should().NotBeNull();
            apiRes.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async void RefreshTokenExpireTest()
        {
            var token = await GetToken();
            await Task.Delay(22000);
            var rtRes = await GetRefreshToken(new UserRefreshTokenRequest
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            });
            rtRes.Should().NotBeNull();
            rtRes.StatusCode.Should().Be(HttpStatusCode.OK);
            var userRefreshTokenResponse = await rtRes.Content.ReadFromJsonAsync<AppResponse<UserRefreshTokenResponse>>();
            userRefreshTokenResponse.Should().NotBeNull();
            userRefreshTokenResponse?.IsSucceed.Should().BeFalse();
            userRefreshTokenResponse?.Data?.Should().BeNull();
            userRefreshTokenResponse?.Messages.Any(m => m.Key == "token").Should().BeTrue();

        }

        private async Task<UserLoginResponse> GetToken()
        {
            var userLoginRequest = new UserLoginRequest
            {
                Email = "UnifiedAppAdmin",
                Password = "UnifiedAppAdmin1!"
            };

            var jsonContent = JsonSerializer.Serialize(userLoginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/User/Login", content);
            var userLoginResponse = await response.Content.ReadFromJsonAsync<AppResponse<UserLoginResponse>>();
            return userLoginResponse?.Data ?? default!;
        }
        private async Task<HttpResponseMessage> GetSecureApiRes(string accessToken)
        {
            HttpRequestMessage request = new(HttpMethod.Post, "/User/Profile");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            return await _client.SendAsync(request);
        }
        private async Task<HttpResponseMessage> GetRefreshToken(UserRefreshTokenRequest userRefreshTokenRequest)
        {

            var jsonContent = JsonSerializer.Serialize(userRefreshTokenRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return await _client.PostAsync("/User/RefreshToken", content);
        }
    }
}