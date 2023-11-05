namespace Service.UserGroup
{
    public class UserLoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class UserLoginResponce
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
    public partial class UserService
    {
        public async Task<AppResponse<UserLoginResponce>> UserLoginAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {

                return new AppResponse<UserLoginResponce>().SetErrorResponce("email", "Email not found");
            }
            else
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
                if (result.Succeeded)
                {
                    var token = await GenerateUserToken(user);
                    return new AppResponse<UserLoginResponce>().SetSuccessResponce(token);
                }
                else
                {
                    return new AppResponse<UserLoginResponce>().SetErrorResponce("password", result.ToString());
                }
            }
        }

    }
}
