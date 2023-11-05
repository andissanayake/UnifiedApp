using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.UserGroup;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
        {
            return await _userService.UserRegisterAsync(req);
        }

        [HttpPost]
        public async Task<AppResponse<UserLoginResponce>> Login(UserLoginRequest req)
        {
            return await _userService.UserLoginAsync(req);
        }

        [HttpPost]
        public async Task<AppResponse<UserRefreshTokenResponce>> RefreshToken(UserRefreshTokenRequest req)
        {
            return await _userService.UserRefreshTokenAsync(req);
        }
        [HttpPost]
        public async Task<AppResponse<bool>> Logout()
        {
            return await _userService.UserLogoutAsync(User);
        }

        [HttpPost]
        [Authorize]
        public string Profile()
        {
            return User.FindFirst("UserName")?.Value ?? "";
        }
    }
}
