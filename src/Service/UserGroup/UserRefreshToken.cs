namespace Service.UserGroup
{
    public class UserRefreshTokenRequest
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
    public class UserRefreshTokenResponce
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
    public partial class UserService
    {
        public async Task<AppResponse<UserRefreshTokenResponce>> UserRefreshTokenAsync(UserRefreshTokenRequest request)
        {
            var principal = TokenUtil.GetPrincipalFromExpiredToken(_appSettings, request.AccessToken);
            if (principal == null || principal.FindFirst("UserName")?.Value == null)
            {
                return new AppResponse<UserRefreshTokenResponce>().SetErrorResponce("email", "User not found");
            }
            else
            {
                var user = await _userManager.FindByNameAsync(principal.FindFirst("UserName")?.Value ?? "");
                if (user == null)
                {
                    return new AppResponse<UserRefreshTokenResponce>().SetErrorResponce("email", "User not found");
                }
                else
                {
                    if (!await _userManager.VerifyUserTokenAsync(user, "APP", "RefreshToken", request.RefreshToken))
                    {
                        return new AppResponse<UserRefreshTokenResponce>().SetErrorResponce("token", "Refresh token expired");
                    }
                    var token = await GenerateUserToken(user);
                    return new AppResponse<UserRefreshTokenResponce>().SetSuccessResponce(new UserRefreshTokenResponce() { AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });
                }
            }
        }
    }
}
