using Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Service.UserGroup
{
    public partial class UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext applicationDbContext,
        AppSettings appSettings)
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly AppSettings _appSettings = appSettings;
        private readonly ApplicationDbContext _context = applicationDbContext;

        private async Task<UserLoginResponse> GenerateUserToken(ApplicationUser user)
        {
            var claims = (from ur in _context.UserRoles
                          where ur.UserId == user.Id
                          join r in _context.Roles on ur.RoleId equals r.Id
                          join rc in _context.RoleClaims on r.Id equals rc.RoleId
                          select rc)
              .Where(rc => rc.ClaimValue != null && rc.ClaimType != null)
              .Select(rc => new Claim(rc.ClaimType ?? "", rc.ClaimValue ?? ""))
              .Distinct()
              .ToList();
            var token = TokenUtil.GetToken(_appSettings, user, claims);
            await _userManager.RemoveAuthenticationTokenAsync(user, "APP", "RefreshToken");
            var refreshToken = await _userManager.GenerateUserTokenAsync(user, "APP", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "APP", "RefreshToken", refreshToken);
            return new UserLoginResponse() { AccessToken = token, RefreshToken = refreshToken };
        }

    }
}
