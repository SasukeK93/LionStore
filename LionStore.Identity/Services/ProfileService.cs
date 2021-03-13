using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using LionStore.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LionStore.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<LionUser> _userManager;

        public ProfileService(UserManager<LionUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
