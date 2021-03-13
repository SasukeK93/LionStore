using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LionStore.Identity.Data
{
    public class DataSeeder
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource{
                  Name = "roles",
                  DisplayName = "LionUser Role(s)",
                  UserClaims = { JwtClaimTypes.Role }
              }
          };
        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               new ApiResource("storeAPI", "Store API"),
               new ApiResource("roles", "LionStore Roles")
          };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "storeClient",
                    ClientName = "Store Client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = new Collection<string> { "https://localhost:5002/swagger/oauth2-redirect.html" },

                    AllowedScopes = { "storeAPI", "roles" }
                }
            };
    }
}
