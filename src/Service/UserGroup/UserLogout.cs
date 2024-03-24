using System.Security.Claims;

namespace Service.UserGroup
{
    public partial class UserService
    {
        public async Task<AppResponse<bool>> UserLogoutAsync(ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated ?? false)
            {
                var username = user.Claims.First(x => x.Type == "UserName").Value;
                var appUser = _context.Users.First(x => x.UserName == username);
                if (appUser != null) { await _userManager.UpdateSecurityStampAsync(appUser); }
                return AppResponse<bool>.SuccessResponse(true);
            }
            return AppResponse<bool>.SuccessResponse(true);
        }
    }
}