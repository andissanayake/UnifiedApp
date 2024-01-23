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
                var appuser = _context.Users.First(x => x.UserName == username);
                if (appuser != null) { await _userManager.UpdateSecurityStampAsync(appuser); }
                return new AppResponse<bool>().SetSuccessResponse(true);
            }
            return new AppResponse<bool>().SetSuccessResponse(true);
        }
    }
}