using Microsoft.AspNetCore.Identity;
using RRS.Api.Data;

namespace RRS.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var userId = user.Id;
                // Use the userId as needed
                return userId;
            }
            return string.Empty;
        }
        
    }
}
