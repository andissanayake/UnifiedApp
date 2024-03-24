using Data;
using Microsoft.AspNetCore.Identity;

namespace Service.UserGroup
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public partial class UserService
    {

        public async Task<AppResponse<bool>> UserRegisterAsync(UserRegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                UserName = request.Email,
                Email = request.Email,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return AppResponse<bool>.SuccessResponse(true);
            }
            else
            {
                return AppResponse<bool>.ErrorResponse(GetRegisterErrors(result));
            }
        }

        private Dictionary<string, string[]> GetRegisterErrors(IdentityResult result)
        {
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return errorDictionary;
        }
    }
}
